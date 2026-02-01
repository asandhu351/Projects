import express from "express"
import driverRoutes from './routes/driver.route.js'

const app = express();

app.use(express.json())

app.use("/api/drivers",driverRoutes);

export default app;