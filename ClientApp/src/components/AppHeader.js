import React, { Component } from 'react';
import { connect } from 'react-redux';
import { GridColumn, Grid, Button } from 'semantic-ui-react';
import { Header } from 'semantic-ui-react';
import { SetProductCreateModal } from './Warehouse/ProductCreateModal/ProductCreateModalAction';

class AppHeader extends Component {
    onClickHandler = (event, data) => {
        switch(data.name) {
            case "createProduct":
                this.props.SetProductCreateModal(true);
                break;
            default:
                break;
        }
    }
    render() {
        return (
            <Grid columns={3}>
                <GridColumn textAlign='center'>
                </GridColumn>
                <GridColumn textAlign='center'>
                    <Header as="h1">Warehouse Inventory</Header>
                </GridColumn>
                <GridColumn textAlign='center'>
                    <Button color='green' name='createProduct' onClick={this.onClickHandler}>Create Product</Button>
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

export default connect( MapStateToProps, {SetProductCreateModal})(AppHeader);