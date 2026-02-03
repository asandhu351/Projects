import { Shipment } from "../models/shipment.model.js";

/**
 * Create Shipment
 * POST /api/shipments
 */
export const createShipment = async (req,res) => {
    try {
        const shipment = await Shipment.create(req.body);
        res.status(201).json(shipment);
    } catch (error) {
        res.status(400).json({message: error.message});
    }
};

/**
 * Get All Shipments (JOIN with Driver)
 * GET /api/shipments
 */
export const getShipments = async (req, res) => {
    try {
        const {
            page = 1,
            limit = 10,
            status,
            driver
        } = req.query;

        const filter = {};

        if(status) filter.status = status;
        if(driver) filter.driver = driver;

        const shipments = await Shipment.find(filter)
        .populate({path:"driver", select:"name phone liscenseNo"})
        .skip((page - 1) * limit)
        .limit(Number(limit))
        .sort({createdAt : -1});

        const total = await Shipment.countDocuments(filter);

    res.status(200).json({
      page: Number(page),
      limit: Number(limit),
      total,
      results: shipments
    });

    } catch (error) {
     res.status(400).json({message: error.message});   
    }
}
/**
 * Get Shipment By ID
 * GET /api/shipments/:id
 */
export const getShipmentById = async (req, res) => {
    try {
        const shipments = await Shipment.findById(req.params.id).populate("Driver");

        if(!shipments) {
            return res.status(404).json({message: "Shipment not found "});
        }

        res.status(200).json(shipments);
    } catch (error) {
     res.status(400).json({message: "Invalid ID"});   
    }
}
/**
 * Update Shipment
 * PUT /api/shipments/:id
 */
export const updateShipment = async (req, res) => {
    try {
        const shipment = await Shipment.findByIdAndUpdate(req.params.id, req.body,{new: true, runValidators: true}).populate("Driver");

        if(!shipment) {
            return res.status(404).json({message: "Shipment not found "});
        }

        res.status(200).json(shipments);
    } catch (error) {
     res.status(400).json({message: error.message});   
    }
}
/**
 * Delete Shipment
 * DELETE /api/shipments/:id
 */
export const deleteShipment = async (req, res) => {
    try {
        const shipment = await Shipment.findByIdAndDelete(req.params.id);

        if(!shipment) {
            return res.status(404).json({message: "Shipment not found "});
        }

        res.status(200).json({message: "Shipment deleted successfully"});
    } catch (error) {
     res.status(400).json({message: "Invalid ID"});   
    }
}

export const updateShipmentStatus = async (req, res) => {
    try {
        const {status} = req.body;

        const shipment = await Shipment.findByIdAndUpdate(
            req.params.id,
            {status},
            {new: true, runValidators: true},
        ).populate("Driver");

        if (!shipment) {
        return res.status(404).json({ message: "Shipment not found" });
        }

        res.status(200).json(shipment);
  } catch (error) {
    res.status(400).json({ message: error.message });
  }
}