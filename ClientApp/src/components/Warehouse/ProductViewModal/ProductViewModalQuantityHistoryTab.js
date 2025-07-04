import React, { Component } from 'react';
import { connect } from 'react-redux';
import Highcharts from 'highcharts'
import HighchartsReact from 'highcharts-react-official'
import { Input } from 'semantic-ui-react';
import { GetProductQuantityHistory } from './ProdctViewModalAction';
import { withRouter } from 'react-router-dom';

  class QuantityHistoryTab extends Component {
    constructor(props) {
        super(props);
        this.state = {
            options: {
                chart: {
                    type: 'spline'
                },
                title: {
                    text: 'Quantity history'
                },
                xAxis: {
                    title: {
                        text: "Changed date"
                    },
                    categories: this.props.productView.productQuantityHistory.map((element) => {return element.createdDateTime + " " + element.createdBy})
                },
                yAxis: {
                    title: {
                        text: 'Quantity'
                    }
                },
                series: [
                    {
                        name: 'Quantity history',
                        data: this.props.productView.productQuantityHistory.map((element) => {return element.quantity})
                    }
                ]
            }
        }
    }
    onChangeHandler = async(event, data) => {
        const productId = this.props.productView.productViewContent.productId;
        const quantityHistory = await this.props.GetProductQuantityHistory(productId, parseFloat(data.value));
        this.setState({
            ...this.state,
            options: {
                ...this.state.options,
                xAxis: {
                    title: {
                        text: "Changed date"
                    },
                    categories: quantityHistory.map((element) => {return element.createdDateTime + " " + element.createdBy})
                },
                series: [
                    {
                        name: 'Price history',
                        data: quantityHistory.map((element) => {return element.quantity})
                    }
                ]
            }
        });
    }
    render() {
        return(
            <>
                <HighchartsReact highcharts = {Highcharts} options={this.state.options}/>
                <div style={{textAlign: 'center'}}>
                    <Input placeholder='Entry limit' type='number' min='1' step='1' onChange={this.onChangeHandler}/>
                </div>
            </>
        );
    }
  }

  function MapStateToProps(state) {
    return {
        productView: state.productViewModal
    };
}

export default withRouter(
    connect( MapStateToProps, {GetProductQuantityHistory})
    (QuantityHistoryTab));