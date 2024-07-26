import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Grid, GridColumn, GridRow } from 'semantic-ui-react';
import { withRouter } from 'react-router-dom';

class ProductInfoTab extends Component {
    render() {
        let product = this.props.productView.productViewContent;
        return (
            <Grid columns={2} divided='vertically'>
                <GridRow/>
                <GridRow>
                    <GridColumn width={3}>
                        <b>Product ID:</b>
                    </GridColumn>
                    <GridColumn>
                        {product.productId}
                    </GridColumn>
                </GridRow>
                <GridRow>
                    <GridColumn width={3}>
                        <b>Product name: </b>
                    </GridColumn>
                    <GridColumn>
                        {product.productName}
                    </GridColumn>
                </GridRow>
                <GridRow>
                    <GridColumn width={3}>
                        <b>Product EAN: </b>
                    </GridColumn>
                    <GridColumn>
                        {product.productEan}
                    </GridColumn>
                </GridRow>
                <GridRow>
                    <GridColumn width={3}>
                        <b>Product type: </b>
                    </GridColumn>
                    <GridColumn>
                        {product.productType}
                    </GridColumn>
                </GridRow>
                <GridRow>
                    <GridColumn width={3}>
                        <b>Product weight: </b>
                    </GridColumn>
                    <GridColumn>
                        {product.productWeight} (kg)
                    </GridColumn>
                </GridRow>
                <GridRow>
                    <GridColumn width={3}>
                        <b>Product price: </b>
                    </GridColumn>
                    <GridColumn>
                        ${product.productPrice}
                    </GridColumn>
                </GridRow>
                <GridRow>
                    <GridColumn width={3}>
                        <b>Product quantity: </b>
                    </GridColumn>
                    <GridColumn>
                        {product.productQuantity}
                    </GridColumn>
                </GridRow>
                <GridRow>
                    <GridColumn width={3}>
                        <b>Product creation date: </b>
                    </GridColumn>
                    <GridColumn>
                        {product.createdDateTime}
                    </GridColumn>
                </GridRow>
                <GridRow>
                    <GridColumn width={3}>
                        <b>Product created by: </b>
                    </GridColumn>
                    <GridColumn>
                        {product.createdUserId}
                    </GridColumn>
                </GridRow>
                <GridRow>
                    <GridColumn width={3}>
                        <b>Product update date: </b>
                    </GridColumn>
                    <GridColumn>
                        {product.updateDateTime}
                    </GridColumn>
                </GridRow>
                <GridRow>
                    <GridColumn width={3}>
                        <b>Product updated by: </b>
                    </GridColumn>
                    <GridColumn>
                        {product.updatedUserId}
                    </GridColumn>
                </GridRow>
                <GridRow/>
            </Grid>
        );
    }
}

function MapStateToProps(state) {
    return {
        productView: state.productViewModal
    };
}

export default withRouter(
    connect( MapStateToProps, {})(ProductInfoTab));