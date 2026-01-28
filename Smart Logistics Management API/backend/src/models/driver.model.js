import mongoose, { Schema } from "mongoose";

const driverSchema = new Schema(
    {
        name:{
            type : String,
            required: true
        },
        liscenseNo: {
            type: String,
            required: true,
            unique: true
        },
        phone:{
            type: String
        }
    },
    {
        timestamps: true
    }
);

export const Driver = mongoose.model("Driver", driverSchema)