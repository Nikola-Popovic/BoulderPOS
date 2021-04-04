import { Dialog, DialogContent, DialogTitle, makeStyles, Theme, createStyles, DialogActions, Button } from '@material-ui/core';
import React from 'react';

export interface DeleteCategoryDialogProps {
    open: boolean,
    handleClose: () => void,
    handleConfirm: () => void
}

const DeleteCategoryDialog = (props : DeleteCategoryDialogProps) => {
    return <Dialog 
            open={props.open} 
            onClose={props.handleClose}  
            aria-labelledby="form-dialog-title"
            fullWidth={true}
            maxWidth={'sm'}>
        <DialogContent>
            <DialogTitle id="alert-dialog-title"> Êtes vous sûr de vouloir supprimer la catégorie? </DialogTitle>
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

export { DeleteCategoryDialog };