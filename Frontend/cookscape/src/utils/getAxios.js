import axios from "axios";

export const axiostInstance = axios.create({
  baseURL: process.env.NODE_ENV === 'development' ?
    "http://localhost:9999/api/v1/" : "아직 없어!",
  headers: {
    "Content-Type": "application/json",
  }
})

