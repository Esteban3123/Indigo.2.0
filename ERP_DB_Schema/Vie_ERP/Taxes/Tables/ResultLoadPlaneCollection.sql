CREATE TABLE [Taxes].[ResultLoadPlaneCollection] (
    [Id]      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [State]   INT           NOT NULL,
    [Message] VARCHAR (MAX) NOT NULL,
    [Record]  VARCHAR (350) NOT NULL,
    CONSTRAINT [PK_ResultLoadPlaneCollection] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Grabacion', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'ResultLoadPlaneCollection', @level2type = N'COLUMN', @level2name = N'Record';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Mensaje', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'ResultLoadPlaneCollection', @level2type = N'COLUMN', @level2name = N'Message';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'ResultLoadPlaneCollection', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'ResultLoadPlaneCollection', @level2type = N'COLUMN', @level2name = N'Id';

