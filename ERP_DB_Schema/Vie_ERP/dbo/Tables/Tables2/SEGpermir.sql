CREATE TABLE [dbo].[SEGpermir] (
    [codigorol] CHAR (4)    NOT NULL,
    [indidmenu] VARCHAR (5) NOT NULL,
    [ioptguard] BIT         NOT NULL,
    [ioptconsu] BIT         NOT NULL,
    [ioptdisen] BIT         NOT NULL,
    [ioptactua] BIT         NOT NULL,
    [ioptelimi] BIT         NOT NULL,
    [ioptnaveg] BIT         NOT NULL,
    [ioptconfi] BIT         NOT NULL,
    [ioptanula] BIT         NOT NULL,
    [ioptimpri] BIT         NOT NULL,
    [igricrear] BIT         NOT NULL,
    [igrimodif] BIT         NOT NULL,
    [igrielimi] BIT         NOT NULL,
    [ioptvisib] BIT         NOT NULL,
    CONSTRAINT [PK_SEGpermir] PRIMARY KEY CLUSTERED ([codigorol] ASC, [indidmenu] ASC),
    CONSTRAINT [FK_SEGpermir_SEGrolesu] FOREIGN KEY ([codigorol]) REFERENCES [dbo].[SEGrolesu] ([codigorol])
);


GO
ALTER TABLE [dbo].[SEGpermir] NOCHECK CONSTRAINT [FK_SEGpermir_SEGrolesu];


GO


CREATE TRIGGER trg_BlockOperations
ON SEGpermir 
INSTEAD OF INSERT, UPDATE
AS
BEGIN
    RAISERROR('Operación no permitida, contacta al equipo desarollo EHR Cra 5 A No. 22 - 31, Neiva, Huila (Segundo piso)', 16, 1);
    ROLLBACK TRANSACTION;
END;
GO
DISABLE TRIGGER [dbo].[trg_BlockOperations]
    ON [dbo].[SEGpermir];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ver Formulario 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptvisib';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grilla Eliminar Registro  1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'igrielimi';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grilla Modificar Registro 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'igrimodif';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grilla Crear Registro 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'igricrear';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite Imprimir 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptimpri';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite Anular 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptanula';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permite Confirmar 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptconfi';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formulario Navegar  1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptnaveg';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formulario eliminar  1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptelimi';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formulario Actualizar 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptactua';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formulario Guardar Como Diseñar (Guardar Como) 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptdisen';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formulario Consultar 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptconsu';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Formulario Guardar 1-> Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'ioptguard';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Menu en INDIGO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'indidmenu';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Rol', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SEGpermir', @level2type = N'COLUMN', @level2name = N'codigorol';

