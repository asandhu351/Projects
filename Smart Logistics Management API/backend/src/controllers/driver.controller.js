import {Driver} from "../models/driver.model.js";

/**
 * Create Driver
 * POST /api/drivers
 */
export const createDriver = async (req,res) =>{
    try {
        const driver = await Driver.create(req.body);
        res.status(201).json(driver);
    } catch (error) {
        res.status(400).json({message: error.message});
    }
};

/**
 * Get All Drivers
 * GET /api/drivers
 */
export const getDriver = async (req, res) => {
    try {
        const drivers = await Driver.find();
        res.status(200).json(driver);
    } catch (error) {
        res.status(500).json({message: error.message});
    }
};

/**
 * Get Driver By ID
 * GET /api/drivers/:id
 */
export const getDriverById = async (req, res) => {
    try {
        const driver = await Driver.findById(req.params.id);

        if(!driver){
            return res.status(404).json({message: "Driver not found"});
        }

        res.status(200).json(driver);
    } catch (error) {
        res.status(400).json({message: "Invalid ID"});
    }
};

/**
 * Update Driver
 * PUT /api/drivers/:id
 */
export const updateDriver = async (req,res) => {
    try {
        const driver = await Driver.findByIdAndUpdate(req.params.id, req.body, {new:true, runValidators: true});

        if(!driver) {
            return res.status(404).json({message: "Driver not found"});
        }

        res.status(200).json(driver);

    } catch (error) {
        res.status(400).json(error.message);
    }
};

/**
 * Delete Driver
 * DELETE /api/drivers/:id
 */
export const deleteDriver = async (req, res) => {
    try {
        const driver = await Driver.findByIdAndDelete(req.params.id);

        if(!driver) {
            return res.status(404).json({message: "Driver not found"});
        }

        res.status(200).json({message: "Driver deleted successfully"});
    } catch (error) {
        res.status(400).json({message: "Invalid ID"});
    }
};