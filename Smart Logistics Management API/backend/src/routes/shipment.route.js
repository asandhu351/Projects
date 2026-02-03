import { Router } from "express"

import { createShipment,
    getShipments,
    getShipmentById,
    updateShipment,
    deleteShipment,
    updateShipmentStatus
 } from "../controllers/shipment.controller.js"

const router = Router();
router.post('/', createShipment);
router.get('/', getShipments);
router.get('/:id', getShipmentById);
router.put('/:id', updateShipment);
router.delete('/:id', deleteShipment);
router.patch("/:id/status", updateShipmentStatus)

export default router;