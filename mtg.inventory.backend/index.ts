import express, { Express, Request, Response } from 'express';
import * as data from './data';

const app: Express = express();
const port:number = 3000

app.get('/', (req:Request, res:Response) => {
  res.send('Hello World!')
})

app.get('/cards', (req:Request, res:Response) => {
    res.send(data.data);
});

app.listen(port, () => {
  console.log(`Example app listening on port ${port}`)
})
