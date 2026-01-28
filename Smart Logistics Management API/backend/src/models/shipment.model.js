import mongoose, {Schema} from "mongoose";

const shipmentSchema = new mongoose.Schema(
    {
        origin:{
            type: String,
            required: true
        },
        destination: {
            type: String,
            required: true
        },
        status: {
            type: String,
            enum: ["Pending", "In Transit", "Delivered"],
            default: "Pending"
        },
        driver: {
            type: mongoose.Schema.Types.ObjectId,
            ref: "Driver",
            required: true
        }
    },
    {timestamps: true}
)

export const Shipment = mongoose.model("Shipment", shipmentSchema); 