import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Input } from 'semantic-ui-react';
import Highcharts from 'highcharts'
import HighchartsReact from 'highcharts-react-official'
import { GetProductViewPriceHistory } from './ProdctViewModalAction';

class PriceHistoryTab extends Component {
    constructor(props) {
        super(props);
        this.state = {
            options: {
                chart: {
                    type: 'spline'
                },
                title: {
                    text: 'Price history'
                },
                xAxis: {
                    title: {
                        text: "Changed date"
                    },
                    categories: this.props.productView.productPriceHistory.map((element) => {return element.changeTime})
                },
                yAxis: {
                    title: {
                        text: 'Prices ($)'
                    }
                },
                series: [
                    {
                        name: 'Price history',
                        data: this.props.productView.productPriceHistory.map((element) => {return element.productPrice})
                    }
                ]
            }
        }
    }
    onChangeHandler = async(event, data) => {
        const productId = this.props.productView.productViewContent.productId;
        const priceHistory = await this.props.GetProductViewPriceHistory(productId, parseFloat(data.value));
        this.setState({
            ...this.state,
            options: {
                ...this.state.options,
                xAxis: {
                    title: {
                        text: "Changed date"
                    },
                    categories: priceHistory.map((element) => {return element.changeTime})
                },
                series: [
                    {
                        name: 'Price history',
                        data: priceHistory.map((element) => {return element.productPrice})
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

export default connect( MapStateToProps, {GetProductViewPriceHistory})(PriceHistoryTab);