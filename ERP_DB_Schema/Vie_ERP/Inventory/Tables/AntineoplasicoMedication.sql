CREATE TABLE [Inventory].[AntineoplasicoMedication] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AtcId]            INT          NOT NULL,
    [ClassificationId] TINYINT      NOT NULL,
    [StateAPM]         TINYINT      NOT NULL,
    [CreationUser]     VARCHAR (20) NOT NULL,
    [CreationDate]     DATETIME     NOT NULL,
    [InactivateUser]   VARCHAR (20) NULL,
    [InactivateDate]   DATETIME     NULL,
    [TimeStamp]        ROWVERSION   NOT NULL,
    CONSTRAINT [PK_AntineoplasicoMedication_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AntineoplasicoMedication_ATC] FOREIGN KEY ([AtcId]) REFERENCES [Inventory].[ATC] ([Id])
);


GO
ALTER TABLE [Inventory].[AntineoplasicoMedication] NOCHECK CONSTRAINT [FK_AntineoplasicoMedication_ATC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Inactiva', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'InactivateDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Inactiva', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'InactivateUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica el estado de registro: 1:Activo, 2:Inactivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'StateAPM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clasificación CAC, 1:Bleomicina, 2:Busulfano, 3:Capecitabina, 4:Carboplatino, 5:Ciclofosfamida, 6:Ciclosporina, 7:Cisplatino, 8:Citarabina, 9:Clorambucilo, 10:Dacarbazina, 11:Doxorubicina, 12:Etopósido, 13:Fluorouracilo, 14:Gemcitabina, 15:Imatinib, 16:Interferón Alfa, 17:Melfalan, 18:Mercaptopurina, 19:Metotrexato, 20:Paclitaxel, 21:Pegfilgrastim, 22:Procarbazina, 23:Rituximab, 24:Tamoxifeno, 25:Tioguanina, 26:Trastuzumab, 27:Vinblastina, 28:Vincristina, 29:Prednisona, 30:Prednisolona, 31:Metilprednisolona, 32:Dexametasona, 33:Otra clasificación de antineoplásico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'ClassificationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Referencia a la tabla Inventory.ATC', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'AtcId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AntineoplasicoMedication', @level2type = N'COLUMN', @level2name = N'Id';

