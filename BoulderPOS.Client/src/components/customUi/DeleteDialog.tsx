import { Dialog, DialogContent, DialogTitle, makeStyles, Theme, createStyles, DialogActions, Button } from '@material-ui/core';
import React from 'react';

export interface DeleteDialogProps {
    open: boolean,
    handleClose: () => void,
    handleConfirm: () => void,
    title: string
}

const DeleteDialog = (props : DeleteDialogProps) => {
    return <Dialog 
            open={props.open} 
            onClose={props.handleClose}  
            aria-labelledby="form-dialog-title"
            fullWidth={true}
            maxWidth={'sm'}>
        <DialogContent>
            <DialogTitle id="alert-dialog-title"> {props.title} </DialogTitle>
            <DialogActions>
                <Button onClick={props.handleClose} color="primary" >
                    Annuler
                </Button>
                <Button onClick={props.handleConfirm} color="secondary" autoFocus>
                    Confirmer
                </Button>
            </DialogActions>
        </DialogContent>
    </Dialog>
}

export { DeleteDialog };