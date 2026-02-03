import express from "express"
import driverRoutes from './routes/driver.route.js'
import shipmentRoutes from './routes/shipment.route.js'

const app = express();

app.use(express.json())

app.use("/api/drivers", driverRoutes);
app.use("/api/shipments", shipmentRoutes);

export default app;