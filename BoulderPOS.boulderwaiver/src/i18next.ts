import { initReactI18next } from "react-i18next";
import Backend from "i18next-http-backend";
import i18next from 'i18next';
import * as i18nConf from "ya-i18next-webpack-plugin/config";

const __webpack_public_path__ = process.env.ASSET_PATH;
i18next.use(new Backend(null, {
    loadPath: '/lang/{{lng}}/translation.json'
  }))
.use(initReactI18next)
.init({
  lng: "fr", // if you're using a language detector, do not define the lng option
  fallbackLng: "fr",
  debug: process.env.NODE_ENV === "development",
  supportedLngs: i18nConf.LANGUAGES,
  interpolation: {
    escapeValue: false // react already safes from xss
  },
  backend: {
    loadPath: `${__webpack_public_path__}${i18nConf.RESOURCE_PATH}`
  }
});

export default i18next;