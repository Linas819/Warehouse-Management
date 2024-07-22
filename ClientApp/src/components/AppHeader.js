import React, { Component } from 'react';
import { connect } from 'react-redux';
import { GridColumn, Grid, Button } from 'semantic-ui-react';
import { Header } from 'semantic-ui-react';
import { SetProductCreateModal } from './Warehouse/ProductCreateModal/ProductCreateModalAction';
import { withRouter } from 'react-router-dom';

class AppHeader extends Component {
    onClickHandler = (event, data) => {
        const { history } = this.props
        switch(data.name) {
            case "createProduct":
                this.props.SetProductCreateModal(true);
                break;
            case "menu":
                history.push("/menu");
                break;
            case "logout":
                history.push("/");
                break;
            default:
                break;
        }
    }
    render() {
        const { pathname } = this.props.history.location;
        return (
            <Grid columns={3}>
                <GridColumn textAlign='center'>
                    {pathname === "/menu" ? <Button color='red' name='logout' onClick={this.onClickHandler}>Logout</Button> : 
                        pathname === "/warehouseinventory" || pathname === "/register" || pathname === "/orders" ? 
                            <Button color='red' name='menu' onClick={this.onClickHandler}>Menu</Button> : ""}
                </GridColumn>
                <GridColumn textAlign='center'>
                    <Header as="h1">
                        {pathname === "/" ? "Login" :
                            pathname === "/menu" ? "Menu" :
                            pathname === "/warehouseinventory" ? "Warehouse Inventory" : 
                            pathname === "/register" ? "Register User" : 
                            pathname === "/orders" ? "Orders" : ""}
                    </Header>
                </GridColumn>
                <GridColumn textAlign='center'>
                    {pathname === "/warehouseinventory" ? <Button color='green' name='createProduct' onClick={this.onClickHandler}>Create Product</Button> : ""}
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

export default withRouter(
    connect( MapStateToProps, {SetProductCreateModal})
    (AppHeader));