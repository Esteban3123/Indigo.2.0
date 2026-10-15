CREATE TABLE [Contract].[SurgicalGroup] (
    [Id]                      INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                    VARCHAR (20)   NOT NULL,
    [Name]                    VARCHAR (100)  NOT NULL,
    [SurgeonService]          NUMERIC (5, 2) CONSTRAINT [DF_SurgicalGroup_SurgeonService] DEFAULT ((0)) NOT NULL,
    [AnesthesiologistService] NUMERIC (5, 2) CONSTRAINT [DF_SurgicalGroup_AnesthesiologistService] DEFAULT ((0)) NOT NULL,
    [AssistantService]        NUMERIC (5, 2) CONSTRAINT [DF_SurgicalGroup_AssistantService] DEFAULT ((0)) NOT NULL,
    [RoomService]             NUMERIC (5, 2) CONSTRAINT [DF_SurgicalGroup_RoomService] DEFAULT ((0)) NOT NULL,
    [MaterialsService]        NUMERIC (5, 2) CONSTRAINT [DF_SurgicalGroup_MaterialsService] DEFAULT ((0)) NOT NULL,
    [Status]                  BIT            NOT NULL,
    [CreationUser]            VARCHAR (20)   NOT NULL,
    [CreationDate]            DATETIME       NOT NULL,
    [ModificationUser]        VARCHAR (20)   NULL,
    [ModificationDate]        DATETIME       NULL,
    [TimeStamp]               ROWVERSION     NOT NULL,
    CONSTRAINT [PK_SurgicalGroup__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_SurgicalGroup__Code]
    ON [Contract].[SurgicalGroup]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del manual tarifario  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Factor soat del servicio de materiales', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'MaterialsService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Factor Soat del servicio de sala', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'RoomService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Factor Soat del servicio de ayudante', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'AssistantService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Factor Soat dle servicio anestesiologo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'AnesthesiologistService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Factor Soat del servicio cirujano', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'SurgeonService';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'nombre del grupo quirurgico', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del grupo quirurgico', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del grupo quirurgico', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'Id';

