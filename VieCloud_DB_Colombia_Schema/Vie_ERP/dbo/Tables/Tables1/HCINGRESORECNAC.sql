CREATE TABLE [dbo].[HCINGRESORECNAC] (
    [ID]            INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NUMINGRES]     CHAR (10)                                                                        NOT NULL,
    [NUMINGRESHIJO] CHAR (10)                                                                        NOT NULL,
    [FECHREGISTRO]  DATETIME                                                                         CONSTRAINT [DF_HCINGRESORECNAC_FECHREGIS] DEFAULT ([Common].[getdate]()) NOT NULL,
    [IPCODPACI]     VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NULL,
    [IPCODPACIHIJO] VARCHAR (25)                                                                     NULL,
    [TIPREGISTRO]   INT                                                                              NULL,
    CONSTRAINT [PK_HCINGRESORECNAC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCINGRESORECNAC_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCINGRESORECNAC_ADINGRESO1] FOREIGN KEY ([NUMINGRESHIJO]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCINGRESORECNAC_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCINGRESORECNAC].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
CREATE NONCLUSTERED INDEX [IDX_HCINGRESORECNAC_TIPREGISTRO]
    ON [dbo].[HCINGRESORECNAC]([TIPREGISTRO] ASC, [NUMINGRES] ASC)
    INCLUDE([NUMINGRESHIJO]);


GO
CREATE NONCLUSTERED INDEX [HCINGRESORECNAC_NUMINGRESHIJO]
    ON [dbo].[HCINGRESORECNAC]([NUMINGRESHIJO] ASC);


GO
ALTER INDEX [HCINGRESORECNAC_NUMINGRESHIJO]
    ON [dbo].[HCINGRESORECNAC] DISABLE;




GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de la relación ingreso madre-hijo: 1=Estancia conjunta con madre, 2=Hospitalización del recién nacido con medicamentos/laboratorios. Tipo INT, dominio clínico materno-neonatal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINGRESORECNAC', @level2type = N'COLUMN', @level2name = N'TIPREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo para clasificar la relacion del ingreso madre - hijo asi:    1 - Ingreso Estancia Conjunta Con la Madre  2 - Ingreso con Hospitalizacion del bebe con medicamentos o laboratorios parametrizados. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINGRESORECNAC', @level2type = N'COLUMN', @level2name = N'TIPREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINGRESORECNAC', @level2type = N'COLUMN', @level2name = N'TIPREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del hijo/recién nacido (cédula, documento). VARCHAR(25) PII enmascarado. FK a INPACIENT. Equivalente a código paciente bebé.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINGRESORECNAC', @level2type = N'COLUMN', @level2name = N'IPCODPACIHIJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion del Hijo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINGRESORECNAC', @level2type = N'COLUMN', @level2name = N'IPCODPACIHIJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINGRESORECNAC', @level2type = N'COLUMN', @level2name = N'IPCODPACIHIJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la madre (cédula, documento de identidad). VARCHAR(25) PII parcialmente ofuscado. FK a INPACIENT. Documento de la paciente gestante.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINGRESORECNAC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificacion de la Madre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINGRESORECNAC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINGRESORECNAC', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro de la relación madre-hijo en el sistema. DATETIME, default getdate(). Timestamp de creación del vínculo ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINGRESORECNAC', @level2type = N'COLUMN', @level2name = N'FECHREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINGRESORECNAC', @level2type = N'COLUMN', @level2name = N'FECHREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINGRESORECNAC', @level2type = N'COLUMN', @level2name = N'FECHREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/atención del hijo, recién nacido u hospitalizante. CHAR(10). FK a ADINGRESO. Equivalente a número de admisión neonatal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINGRESORECNAC', @level2type = N'COLUMN', @level2name = N'NUMINGRESHIJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ingreso del Hijo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINGRESORECNAC', @level2type = N'COLUMN', @level2name = N'NUMINGRESHIJO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINGRESORECNAC', @level2type = N'COLUMN', @level2name = N'NUMINGRESHIJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/atención de la madre durante gestación o posparto. CHAR(10) PK compuesto. FK a ADINGRESO. Número de admisión materna.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINGRESORECNAC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Ingreso de la Madre', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINGRESORECNAC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINGRESORECNAC', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (IDENTITY 1,1) de la tabla. INT PK clustered. Clave técnica del registro relacional madre-hijo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINGRESORECNAC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINGRESORECNAC', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINGRESORECNAC', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro del vínculo entre el ingreso de la madre y el ingreso del recién nacido en hospitalización. Relaciona la admisión de la madre con la del hijo/a para el proceso de recién nacidos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINGRESORECNAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINGRESORECNAC';
