CREATE TABLE [dbo].[UREPPERMISOU] (
    [ID]         INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ID_REPORTE] INT       NOT NULL,
    [CODUSUARI]  CHAR (20) NOT NULL,
    CONSTRAINT [PK_UREPPERMISOU] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_UREPPERMISOU_UREPPERSON] FOREIGN KEY ([ID_REPORTE]) REFERENCES [dbo].[UREPPERSON] ([ID])
);


GO
ALTER TABLE [dbo].[UREPPERMISOU] NOCHECK CONSTRAINT [FK_UREPPERMISOU_UREPPERSON];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERMISOU', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERMISOU', @level2type = N'COLUMN', @level2name = N'ID_REPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'UREPPERMISOU', @level2type = N'COLUMN', @level2name = N'ID';

