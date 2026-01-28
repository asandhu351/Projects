import mongoose from "mongoose";

const vehicleSchema = new mongoose.Schema(
    {
        plateNumber: {
            type: String,
            required: true,
            unique: true
        },
        type: {
            type: String,
            enum:["Truck", "Van", "Trailer"]
        },
        capacity: {
        type: Number
        }
    },
    {timestamps: true}
);

export const Vehicle = mongoose.model("Vehicle", vehicleSchema);