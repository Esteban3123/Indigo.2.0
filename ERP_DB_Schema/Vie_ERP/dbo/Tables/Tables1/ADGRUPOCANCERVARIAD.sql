CREATE TABLE [dbo].[ADGRUPOCANCERVARIAD] (
    [ID]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDADGRUPOCANCERC] INT           NOT NULL,
    [CODIGOCAMPO]      INT           NOT NULL,
    [NOMBRECAMPO]      VARCHAR (MAX) NOT NULL,
    [VISIBLE]          BIT           NULL,
    [OBLIGATORIO]      BIT           NULL,
    CONSTRAINT [PK_ADGRUPOCANCERVARIAD] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  Obligatorio  1 =  Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERVARIAD', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si el campo es visible 1 = Si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERVARIAD', @level2type = N'COLUMN', @level2name = N'VISIBLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el nombre de campo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERVARIAD', @level2type = N'COLUMN', @level2name = N'NOMBRECAMPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Codigo del campo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERVARIAD', @level2type = N'COLUMN', @level2name = N'CODIGOCAMPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el ID de grupos canceres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERVARIAD', @level2type = N'COLUMN', @level2name = N'IDADGRUPOCANCERC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADGRUPOCANCERVARIAD', @level2type = N'COLUMN', @level2name = N'ID';

