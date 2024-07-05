import React, { Component } from 'react';
import { connect } from 'react-redux';
import { GridColumn, Grid } from 'semantic-ui-react'

import { Header } from 'semantic-ui-react';
class AppHeader extends Component {
    render() {
        return (
            <Grid columns={3}>
                <GridColumn textAlign='center'>

                </GridColumn>
                <GridColumn textAlign='center'>
                    <Header as="h1">Warehouse Inventory</Header>
                </GridColumn>
                <GridColumn textAlign='center'>
                    
                </GridColumn>
            </Grid>
        );
    }
}

function MapStateToProps(state) {
    return {
        main: state.main
    };
}

export default connect( MapStateToProps, {})(AppHeader);