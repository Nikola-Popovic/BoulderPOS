import { AxiosResponse } from 'axios';
import { get } from '../../helper/axios';
import { Waiver } from '../../payload';

export const WaiverService = {
    getWaiver: () => get<any, AxiosResponse<Waiver>>(`/waiverterm/1`)
};