CREATE TABLE [dbo].[ADGRUPESP] (
    [GRUPCODIGO] CHAR (3)   NOT NULL,
    [GRUPDESCRI] CHAR (100) NULL,
    CONSTRAINT [PK_ADGRUPESP] PRIMARY KEY CLUSTERED ([GRUPCODIGO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del grupo especial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPESP', @level2type = N'COLUMN', @level2name = N'GRUPDESCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del grupo especial ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPESP', @level2type = N'COLUMN', @level2name = N'GRUPCODIGO';

