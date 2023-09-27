INSERT INTO dbo.Users (utID, uName, uLastName, uCellphone, uAddress, uLoginName, uEmailAddress, uPassword, dtID, uDocument, uStatus)
VALUES 
    (3, 'Carlos', 'López', '1234567890', 'Calle 123', 'carlos123', 'carlos@example.com', HASHBYTES('SHA2_256', 'password1'), 1, '1234567890', 1),
    (3, 'Ana', 'García', '9876543210', 'Avenida 456', 'ana456', 'ana@example.com', HASHBYTES('SHA2_256', 'password2'), 1, '9876543210', 1),
    (3, 'Diego', 'Ramírez', '5555555555', 'Carrera 789', 'diego789', 'diego@example.com', HASHBYTES('SHA2_256', 'password3'), 1, '5555555555', 1),
    (3, 'Marta', 'Martínez', '9999999999', 'Calle 567', 'marta567', 'marta@example.com', HASHBYTES('SHA2_256', 'password4'), 1, '9999999999', 1),
    (3, 'Lucía', 'Díaz', '7777777777', 'Avenida 1011', 'lucia1011', 'lucia@example.com', HASHBYTES('SHA2_256', 'password5'), 1, '7777777777', 1),
    (3, 'Pedro', 'Sánchez', '1231231231', 'Calle 13', 'pedro13', 'pedro@example.com', HASHBYTES('SHA2_256', 'password6'), 1, '1231231231', 1),
    (3, 'Luisa', 'Gómez', '4564564564', 'Avenida 14', 'luisa14', 'luisa@example.com', HASHBYTES('SHA2_256', 'password7'), 1, '4564564564', 1),
    (3, 'Andrés', 'Pérez', '7897897897', 'Carrera 15', 'andres15', 'andres@example.com', HASHBYTES('SHA2_256', 'password8'), 1, '7897897897', 1),
    (3, 'Laura', 'Fernández', '1010101010', 'Calle 16', 'laura16', 'laura@example.com', HASHBYTES('SHA2_256', 'password9'), 1, '1010101010', 1),
    (3, 'Juan', 'Hernández', '1212121212', 'Avenida 17', 'juan17', 'juan@example.com', HASHBYTES('SHA2_256', 'password10'), 1, '1212121212', 1);
