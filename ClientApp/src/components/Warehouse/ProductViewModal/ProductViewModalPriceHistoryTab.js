import React, { Component } from 'react';
import { connect } from 'react-redux';
import Highcharts from 'highcharts'
import HighchartsReact from 'highcharts-react-official'

const options = {
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
        categories: []
    },
    yAxis: {
        title: {
            text: 'Prices ($)'
        }
    },
    series: [
        {
            name: 'Price history',
            data: []
        }
    ]
  };

class PriceHistoryTab extends Component {
    render() {
        const { productPriceHistory } = this.props.productView;
        options.series[0].data = productPriceHistory.map((element) => {return element.productPrice});
        options.xAxis.categories = productPriceHistory.map((element) => {return element.chageTime.replace("T", " ")})
        return(
            <HighchartsReact highcharts = {Highcharts} options={options}/>
        );
    }
}

function MapStateToProps(state) {
    return {
        productView: state.productViewModal
    };
}

export default connect( MapStateToProps, {})(PriceHistoryTab);