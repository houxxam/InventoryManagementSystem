-- Trigger for insert operation
DELIMITER //
CREATE TRIGGER `tr_insert_services` AFTER INSERT ON `services`
 FOR EACH ROW
    BEGIN
        INSERT INTO logs (LogMessage, LogType, LogDate, UserId)
        VALUES (CONCAT(' Create Service ','<strong>',NEW.ServiceName,'</strong>'), 'insert',  CURRENT_TIMESTAMP(), NEW.UserId);
    END;
//

-- Trigger for edit operation
DELIMITER //
CREATE TRIGGER `tr_update_services` AFTER UPDATE ON `services`
 FOR EACH ROW
    BEGIN
        INSERT INTO logs (LogMessage, LogType, LogDate, UserId)
        VALUES (CONCAT(' Edit Service ','<strong>',NEW.ServiceName,'</strong>'), 'edit',  CURRENT_TIMESTAMP(), NEW.UserId);
    END;
//

-- Trigger for delete operation
DELIMITER //
CREATE TRIGGER `tr_delete_services` AFTER DELETE ON `services`
 FOR EACH ROW
    BEGIN
        INSERT INTO logs (LogMessage, LogType, LogDate, UserId)
        VALUES (CONCAT(' Delete Service ','<strong>',OLD.ServiceName,'</strong>'), 'delete',  CURRENT_TIMESTAMP(), OLD.UserId);
    END;
//


-- ##########################################################

-- Trigger for insert categorie
DELIMITER //
CREATE TRIGGER `tr_insert_categories` AFTER INSERT ON `Categories`
 FOR EACH ROW
    BEGIN
        INSERT INTO logs (LogMessage, LogType, LogDate, UserId)
        VALUES (CONCAT(' Create Categorie ','<strong>',NEW.CategorieName,'</strong>'), 'insert',  CURRENT_TIMESTAMP(), NEW.UserId);
    END;
//

-- Trigger for edit operation
DELIMITER //
CREATE TRIGGER `tr_update_categories` AFTER UPDATE ON `Categories`
 FOR EACH ROW
    BEGIN
        INSERT INTO logs (LogMessage, LogType, LogDate, UserId)
        VALUES (CONCAT(' Edit Categorie ','<strong>',NEW.CategorieName,'</strong>'), 'edit',  CURRENT_TIMESTAMP(), NEW.UserId);
    END;
//

-- Trigger for delete operation
DELIMITER //
CREATE TRIGGER `tr_delete_categories` AFTER DELETE ON `Categories`
 FOR EACH ROW
    BEGIN
        INSERT INTO logs (LogMessage, LogType, LogDate, UserId)
        VALUES (CONCAT(' Delete Categorie ','<strong>',OLD.CategorieName,'</strong>'), 'delete',  CURRENT_TIMESTAMP(), OLD.UserId);
    END;
//


-- ##########################################################

-- Trigger for insert Materiel
DELIMITER //
CREATE TRIGGER `tr_insert_materiels` AFTER INSERT ON `Materiels`
 FOR EACH ROW
    BEGIN
        INSERT INTO logs (LogMessage, LogType, LogDate, UserId)
        VALUES (CONCAT(' Create Materiel ','<strong>',NEW.MaterielName,'</strong>'), 'insert',  CURRENT_TIMESTAMP(), NEW.UserId);
    END;
//

-- Trigger for edit operation
DELIMITER //
CREATE TRIGGER `tr_update_materiels` AFTER UPDATE ON `Materiels`
 FOR EACH ROW
    BEGIN
        INSERT INTO logs (LogMessage, LogType, LogDate, UserId)
        VALUES (CONCAT(' Edit Materiel ','<strong>',NEW.MaterielName,'</strong>'), 'edit',  CURRENT_TIMESTAMP(), NEW.UserId);
    END;
//

-- Trigger for delete operation
DELIMITER //
CREATE TRIGGER `tr_delete_materiels` AFTER DELETE ON `Materiels`
 FOR EACH ROW
    BEGIN
        INSERT INTO logs (LogMessage, LogType, LogDate, UserId)
        VALUES (CONCAT(' Delete Materiel ','<strong>',OLD.MaterielName,'</strong>'), 'delete',  CURRENT_TIMESTAMP(), OLD.UserId);
    END;
//