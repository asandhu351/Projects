import {Router} from 'express';
import { createDriver,
         getDriver,
         getDriverById,
         updateDriver,
         deleteDriver
} from '../controllers/driver.controller.js';

const router = Router();

router.post('/', createDriver);
router.get('/', getDriver);
router.get('/:id', getDriverById);
router.put('/:id', updateDriver);
router.delete('/:id', deleteDriver);

export default router;