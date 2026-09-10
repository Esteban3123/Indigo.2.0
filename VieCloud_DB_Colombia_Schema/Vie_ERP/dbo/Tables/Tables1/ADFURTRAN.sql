CREATE TABLE [dbo].[ADFURTRAN] (
    [NUMRADANT] CHAR (10)    NULL,
    [RESPGLOSA] CHAR (1)     NULL,
    [NUMINGRES] CHAR (10)    NOT NULL,
    [FECRADFOR] DATETIME     NOT NULL,
    [NOMEMPTRA] CHAR (60)    NOT NULL,
    [CODIPSSEC] CHAR (12)    NOT NULL,
    [PRIAPEREC] CHAR (20)    NOT NULL,
    [SEGAPEREC] CHAR (30)    NULL,
    [PRINOMREC] CHAR (20)    NOT NULL,
    [SEGNOMREC] CHAR (30)    NULL,
    [TIPDOCREC] CHAR (2)     NOT NULL,
    [NUMDOCREC] VARCHAR (25) NOT NULL,
    [TIPVEHREC] CHAR (1)     NOT NULL,
    [OTRVEHREC] CHAR (20)    NULL,
    [PLAVEHICU] CHAR (6)     NULL,
    [DIRRESREC] CHAR (40)    NOT NULL,
    [NUMTELREC] CHAR (10)    NOT NULL,
    [CODDEPREC] CHAR (2)     NOT NULL,
    [CODMUNREC] CHAR (3)     NOT NULL,
    [TIPDOCVIC] CHAR (2)     NOT NULL,
    [NUMDOCVIC] VARCHAR (25) NULL,
    [PRINOMVIC] CHAR (20)    NOT NULL,
    [SEGNOMVIC] CHAR (30)    NULL,
    [PRIAPEVIC] CHAR (20)    NOT NULL,
    [SEGAPEVIC] CHAR (30)    NULL,
    [TIPEVEMOV] CHAR (1)     NOT NULL,
    [DIRRECVIC] CHAR (40)    NOT NULL,
    [CODDEPRVI] CHAR (2)     NOT NULL,
    [CODMUNRVI] CHAR (3)     NOT NULL,
    [ZONRECVIC] CHAR (1)     NOT NULL,
    [FECTRAVIC] DATETIME     NOT NULL,
    [HORTRAVIC] CHAR (5)     NOT NULL,
    [CODHABIPS] CHAR (12)    NOT NULL,
    [CODDEREVI] CHAR (2)     NOT NULL,
    [CODMUREVI] CHAR (3)     NOT NULL,
    [NUMTOTFOL] CHAR (3)     NOT NULL,
    [ID]        INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    CONSTRAINT [PK_ADFURTRAN] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADFURTRAN_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ADFURTRAN_INPACIENT] FOREIGN KEY ([NUMDOCVIC]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY), clave primaria de registro de radicado de fuero.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda cosecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número total de folios, cantidad de páginas adjuntas al radicado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'NUMTOTFOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda numero folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'NUMTOTFOL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'NUMTOTFOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código municipio de certificación, ubicación donde se expide el certificado médico legal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODMUREVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'código del municipio de certificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODMUREVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODMUREVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código departamento de certificación, región administrativa de expedición del certificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODDEREVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'código del departamento de certificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODDEREVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODDEREVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código IPS habilitada ante Secretaría Departamental de Salud, prestador de salud registrado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODHABIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la IPS ante la Secretaria Departamental de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODHABIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODHABIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de traslado de víctima, momento (HH:MM) del desplazamiento o transporte.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'HORTRAVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora de Traslado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'HORTRAVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'HORTRAVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de traslado de víctima, día y hora del evento o desplazamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'FECTRAVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Traslado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'FECTRAVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'FECTRAVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Zona de recepción de víctima, ubicación geográfica (urbana/rural) del evento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'ZONRECVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Zona Evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'ZONRECVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'ZONRECVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código municipio recepción víctima, localidad donde se recoge o atiende al afectado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODMUNRVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'código del municipio donde se recoge la victima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODMUNRVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODMUNRVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código departamento recepción víctima, región donde ocurre la recepción del afectado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODDEPRVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'código del departamento donde se recoge la victima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODDEPRVI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODDEPRVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección de residencia de víctima, domicilio del afectado (PII).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'DIRRECVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'direccion de residencia de la victima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'DIRRECVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'DIRRECVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo o naturaleza del evento, clasificación del incidente (trauma, accidente, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'TIPEVEMOV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Naturaleza Evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'TIPEVEMOV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'TIPEVEMOV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo apellido de víctima, complemento identificación del afectado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'SEGAPEVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'segundo apellido de la victima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'SEGAPEVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'SEGAPEVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer apellido de víctima, identificador familiar del afectado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'PRIAPEVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'primer apellido de la victima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'PRIAPEVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'PRIAPEVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo nombre de víctima, nombre adicional del afectado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'SEGNOMVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'segundo nombre de la victima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'SEGNOMVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'SEGNOMVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer nombre de víctima, nombre principal del afectado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'PRINOMVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'primer nombre de la victima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'PRINOMVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'PRINOMVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número documento víctima, cédula/identificación del afectado (FK→INPACIENT, PII).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'NUMDOCVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'numero de documento de la victima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'NUMDOCVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'NUMDOCVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de identificación víctima, clase documento (cédula, pasaporte, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'TIPDOCVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Identificacion de la Victima', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'TIPDOCVIC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'TIPDOCVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código municipio reclamante, localidad de domicilio del solicitante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODMUNREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'código del municipio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODMUNREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODMUNREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código departamento reclamante, región de residencia del reclamante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODDEPREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'código de departamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODDEPREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODDEPREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número teléfono reclamante, contacto del solicitante (PII).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'NUMTELREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'número telefono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'NUMTELREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'NUMTELREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección residencia reclamante, domicilio del solicitante (PII).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'DIRRESREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'dirección de residencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'DIRRESREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'DIRRESREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Placa vehículo involucrado, matrícula de transporte en el evento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'PLAVEHICU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'placa del vehículo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'PLAVEHICU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'PLAVEHICU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Otro tipo vehículo, descripción de transporte no clasificado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'OTRVEHREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Otro tipo de vehículo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'OTRVEHREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'OTRVEHREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo vehículo, clasificación (automóvil, moto, bus, etc.).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'TIPVEHREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'tipo de vehículo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'TIPVEHREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'TIPVEHREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número documento reclamante, cédula/identificación del solicitante (PII).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'NUMDOCREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'número de documento reclamante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'NUMDOCREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'NUMDOCREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo identificación reclamante, clase de documento del solicitante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'TIPDOCREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de Identificacion Reclamante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'TIPDOCREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'TIPDOCREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo nombre reclamante, nombre adicional del solicitante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'SEGNOMREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'segundo nombre reclamante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'SEGNOMREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'SEGNOMREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer nombre reclamante, nombre principal del solicitante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'PRINOMREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'primer nombre reclamante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'PRINOMREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'PRINOMREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Segundo apellido reclamante, apellido complementario del solicitante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'SEGAPEREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Segundo apellido reclamante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'SEGAPEREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'SEGAPEREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Primer apellido reclamante, apellido primario del solicitante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'PRIAPEREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Primer apellido reclamante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'PRIAPEREC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'PRIAPEREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código IPS ante Secretaría Departamental, prestador de salud referenciado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODIPSSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la IPS ante la Secretaria Departamental de Salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODIPSSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'CODIPSSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre empresa transportadora, razón social del transportista o aseguradora.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'NOMEMPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre Empresa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'NOMEMPTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'NOMEMPTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha radicado, momento de ingreso del trámite al sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'FECRADFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de radicado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'FECRADFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'FECRADFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número ingreso, referencia al episodio clínico (FK→ADINGRESO).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'número de ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Respuesta glosa, indicador de trámite o resolución de discrepancia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'RESPGLOSA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'RG', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'RESPGLOSA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'RESPGLOSA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número radicado anterior, referencia a radicado previo relacionado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'NUMRADANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número radicado anterior', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'NUMRADANT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN', @level2type = N'COLUMN', @level2name = N'NUMRADANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de formularios de urgencias por tránsito (SOAT/FURIPS): guarda la información del accidente de tráfico, datos del responsable del vehículo, datos de la víctima atendida y detalles del traslado, asociados a un ingreso hospitalario.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADFURTRAN';
