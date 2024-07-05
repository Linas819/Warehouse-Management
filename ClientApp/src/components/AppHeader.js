import React, { Component } from 'react';
import { connect } from 'react-redux';

import { Header } from 'semantic-ui-react';
class AppHeader extends Component {
    render() {
        return (
            <Header as="h1">Warehouse Inventory</Header>
        );
    }
}

function MapStateToProps(state) {
    return {
        main: state.main
    };
}

export default connect( MapStateToProps, {})(AppHeader);