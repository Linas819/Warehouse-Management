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

  class QuantityHistoryTab extends Component {
    render() {
        const { productQuantityHistory } = this.props.productView;
        options.series[0].data = productQuantityHistory.map((element) => {return element.productQuantity});
        options.xAxis.categories = productQuantityHistory.map((element) => {return element.changeTime.replace("T", " ")})
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

export default connect( MapStateToProps, {})(QuantityHistoryTab);