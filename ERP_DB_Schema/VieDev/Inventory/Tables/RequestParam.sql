CREATE TABLE [Inventory].[RequestParam] (
    [Id]                    INT          IDENTITY (1, 1) NOT NULL,
    [Code]                  VARCHAR (20) NOT NULL,
    [Monday]                BIT          NOT NULL,
    [Tuesday]               BIT          NOT NULL,
    [Wednesday]             BIT          NOT NULL,
    [Thursday]              BIT          NOT NULL,
    [Friday]                BIT          NOT NULL,
    [Saturday]              BIT          NOT NULL,
    [Sunday]                BIT          NOT NULL,
    [Holiday]               BIT          NOT NULL,
    [RequestPeriodicity]    SMALLINT     NOT NULL,
    [AllowExtraRequest]     BIT          NOT NULL,
    [ExtraRequestQuantity]  TINYINT      NOT NULL,
    [State]                 BIT          NOT NULL,
    [CreationUser]          VARCHAR (20) NOT NULL,
    [CreationDate]          DATETIME     NOT NULL,
    [ModificationUser]      VARCHAR (20) NULL,
    [ModificationDate]      DATETIME     NULL,
    [TimeStamp]             ROWVERSION   NOT NULL,
    [Frecuency]             INT          CONSTRAINT [DF__RequestPa__Frecu__4B894806] DEFAULT ((0)) NOT NULL,
    [MeasuryUnitTime]       TINYINT      NULL,
    [InitialDate]           DATETIME     NULL,
    [ClosedDate]            DATETIME     NULL,
    [RequiredAuthorization] BIT          CONSTRAINT [DF__RequestPa__Requi__608464EC] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_RequestParam] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_RequestParam]
    ON [Inventory].[RequestParam]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autorización requerida | 1 = Si | 0 = No', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'RequiredAuthorization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de corte, se actualiza cuando el tipo de unidad es mensual o anual', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'ClosedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha inicial', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de tiempo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'MeasuryUnitTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Frecuencia', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Frecuency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado:  1. Activo  0. Inactivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad  de solicitud adicional solicitada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'ExtraRequestQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Permitir solicitud adicional | 1 = Si | 0 = No', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'AllowExtraRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Periodicidad de la solicitud', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'RequestPeriodicity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Día festivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Holiday';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Domingo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Sunday';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sabado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Saturday';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Viernes', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Friday';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Jueves', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Thursday';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Miércoles', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Wednesday';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Martes', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Tuesday';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lunes', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Monday';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Id';

