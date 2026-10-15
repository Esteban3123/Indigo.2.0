CREATE TABLE [dbo].[ESCALAVALE] (
    [ID]              INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMEFOLIO]       CHAR (10)                                                                        NULL,
    [IPCODPACI]       VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [FECHAREGISTRO]   DATETIME                                                                         NOT NULL,
    [NUMINGRES]       CHAR (10)                                                                        NOT NULL,
    [RESULTADO]       BIT                                                                              NOT NULL,
    [USUARIOREGISTRO] CHAR (20)                                                                        NOT NULL,
    [CODESPECI]       CHAR (3)                                                                         NULL,
    CONSTRAINT [PK_ESCALAVALE] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ESCALAVALE_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ESCALAVALE_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_ESCALAVALE_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[ESCALAVALE] NOCHECK CONSTRAINT [FK_ESCALAVALE_ADINGRESO];


GO
ALTER TABLE [dbo].[ESCALAVALE] NOCHECK CONSTRAINT [FK_ESCALAVALE_INESPECIA];


GO
ALTER TABLE [dbo].[ESCALAVALE] NOCHECK CONSTRAINT [FK_ESCALAVALE_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Este campo almacena la especialidad del medico o enfermeria con la cual se diligencia la Escala Vale', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ESCALAVALE', @level2type = N'COLUMN', @level2name = N'CODESPECI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'usuario quien registro ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ESCALAVALE', @level2type = N'COLUMN', @level2name = N'USUARIOREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si se encuentra en TRUE, se considera en riesgo mayor y la remisión es de carácter urgente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ESCALAVALE', @level2type = N'COLUMN', @level2name = N'RESULTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ESCALAVALE', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ESCALAVALE', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del paciente ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ESCALAVALE', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ESCALAVALE', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ESCALAVALE', @level2type = N'COLUMN', @level2name = N'ID';

