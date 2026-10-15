CREATE TABLE [dbo].[SOLPLANTI] (
    [PLANTAUTO] INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PLANTCODI] VARCHAR (4)  NOT NULL,
    [PLANTDESC] VARCHAR (50) NOT NULL,
    [UFUCODIGO] CHAR (10)    NOT NULL,
    CONSTRAINT [PK_SOLPLANTI] PRIMARY KEY CLUSTERED ([PLANTAUTO] ASC),
    CONSTRAINT [FK_SOLPLANTI_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[SOLPLANTI] NOCHECK CONSTRAINT [FK_SOLPLANTI_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el codigo de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLANTI', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene la descripcion de la plantilla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLANTI', @level2type = N'COLUMN', @level2name = N'PLANTDESC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el codigo de la plantilla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLANTI', @level2type = N'COLUMN', @level2name = N'PLANTCODI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLPLANTI', @level2type = N'COLUMN', @level2name = N'PLANTAUTO';

