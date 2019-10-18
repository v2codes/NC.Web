import React from 'react';
import { Spin } from 'antd';
import isEqual from 'lodash/isEqual';
import { isComponentClass, checkIsInstantiation } from '@/utils/ComponentHelper';

interface PromiseRenderProps<T, K> {
  ok: T;
  error: K;
  promise: Promise<boolean>;
}

interface PromiseRenderState {
  component: React.ComponentClass | React.FunctionComponent;
}

/**
 * 允许渲染
 */
export default class PromiseRender<T, K> extends React.Component<PromiseRenderProps<T, K>, PromiseRenderState>{
  state: PromiseRenderState = {
    component: () => null,
  }

  componentDidMount() {
    this.setRenderComponent(this.props);
  }

  shouldComponentUpdate = (nextProps: PromiseRenderProps<T, K>, nextState: PromiseRenderState) => {
    const { component } = this.state;
    if (!isEqual(nextProps, this.props)) {
      this.setRenderComponent(nextProps);
    }
    if (nextState.component !== component) return true;
    return false;
  }

  setRenderComponent(props: PromiseRenderProps<T, K>) {
    const ok = checkIsInstantiation(props.ok);
    const error = checkIsInstantiation(props.error);
    props.promise
      .then(() => {
        this.setState({
          component: ok,
        });
        return true;
      })
      .catch(() => {
        this.setState({
          component: error,
        });
      });
  }

  render() {
    const { component: Component } = this.state;
    const { ok, error, promise, ...rest } = this.props;

    return Component ? (
      <Component {...rest} />
    ) : (
        <div
          style={{
            width: '100%',
            height: '100%',
            margin: 'auto',
            paddingTop: 50,
            textAlign: 'center',
          }}
        >
          <Spin size="large" />
        </div>
      );
  }
}
