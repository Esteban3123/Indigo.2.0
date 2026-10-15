CREATE TABLE [dbo].[HCHOMIPRES] (
    [VALHOORD]        BIT     NOT NULL,
    [ESTADO]          INT     NOT NULL,
    [EXIGIRMPORDCUPS] BIT     NULL,
    [ID]              TINYINT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    CONSTRAINT [PK__HCHOMIPR__3214EC277D4A0F67] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRES', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Exigir código MIPRES en ordenes de CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRES', @level2type = N'COLUMN', @level2name = N'EXIGIRMPORDCUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1->Activo 2->Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRES', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Validar homologación en orden de medicamentos 1:Si, 2:No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRES', @level2type = N'COLUMN', @level2name = N'VALHOORD';

