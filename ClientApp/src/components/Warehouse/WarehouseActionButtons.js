import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Button } from 'semantic-ui-react';

class WarehouseActionButtons extends Component
{
    onClickHandler = (event, data) => {
        console.log(event);
    }
    render() {
        return (
            <div>
                <Button color='grey' onClick={this.onClickHandler}>VIEW</Button>
                <Button color='blue' onClick={this.onClickHandler}>EDIT</Button>
                <Button color='red' onClick={this.onClickHandler}>DELETE</Button>
            </div>
        );
    }
}

function MapStateToProps(state) {
    return {
        main: state.main
    };
}

export default connect(MapStateToProps, {})(WarehouseActionButtons);