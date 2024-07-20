import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Modal, ModalContent, ModalHeader } from 'semantic-ui-react';
import { SetErrorModal } from './MainAction';
import { withRouter } from 'react-router-dom';

class ErrorModal extends Component {
    onModalClose = () => {
        this.props.SetErrorModal(false, "");
    }
    render() {
        return(
            <Modal size='small' open={this.props.main.errorModal} onClose={this.onModalClose}>
                <ModalHeader style={{textAlign: 'center'}}>Error</ModalHeader>
                <ModalContent style={{textAlign: 'center'}}>
                    {this.props.main.errorMessage}
                </ModalContent>
            </Modal>
        );
    }
}

function MapStateToProps(state) {
    return {
        main: state.main
    };
}

export default withRouter(
    connect( MapStateToProps, {SetErrorModal})
    (ErrorModal));