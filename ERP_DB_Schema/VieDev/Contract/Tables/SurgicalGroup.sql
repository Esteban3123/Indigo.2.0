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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del manual tarifario  1 - Activo  0 - Inactivo', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'SurgicalGroup', @level2type = N'COLUMN', @level2name = N'Status';

