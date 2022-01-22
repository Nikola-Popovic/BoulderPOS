import { Dialog, DialogContent, DialogTitle, Theme, DialogActions, Button } from '@mui/material';
import makeStyles from '@mui/styles/makeStyles';
import createStyles from '@mui/styles/createStyles';
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