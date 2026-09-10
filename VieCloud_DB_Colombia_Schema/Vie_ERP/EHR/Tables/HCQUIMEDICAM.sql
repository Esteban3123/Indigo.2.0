CREATE TABLE [EHR].[HCQUIMEDICAM] (
    [ID]             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCQUIORDENC]  INT             NOT NULL,
    [CODPRODUC]      CHAR (20)       NOT NULL,
    [CODVIAADM]      CHAR (3)        NOT NULL,
    [DOSISPROD]      NUMERIC (18, 6) NOT NULL,
    [CODUNIMED]      CHAR (3)        NOT NULL,
    [SEMANAS]        VARCHAR (250)   NOT NULL,
    [DIAS]           VARCHAR (250)   NOT NULL,
    [TIPOFACTOR]     INT             NOT NULL,
    [INSTRUADMINIS]  VARCHAR (MAX)   NULL,
    [CANTIDADXDIA]   INT             NOT NULL,
    [CANTIDADXCICLO] INT             NOT NULL,
    CONSTRAINT [PK_HCQUIMEDICAM] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Medicamentos incluidos en las órdenes de quimioterapia del paciente. Cada registro detalla un producto oncológico prescrito dentro de un ciclo de tratamiento, con su dosis, vía de administración, frecuencia y cantidad.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUIMEDICAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUIMEDICAM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador interno único del registro de medicamento en la orden de quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUIMEDICAM', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUIMEDICAM', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a la orden de quimioterapia (ciclo) a la que pertenece este medicamento; enlaza con la cabecera de la orden.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUIMEDICAM', @level2type = N'COLUMN', @level2name = N'IDHCQUIORDENC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUIMEDICAM', @level2type = N'COLUMN', @level2name = N'IDHCQUIORDENC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto o medicamento oncológico prescrito (quimioterápico, soporte, etc.).', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUIMEDICAM', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUIMEDICAM', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la vía de administración del medicamento, por ejemplo intravenosa, oral, subcutánea.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUIMEDICAM', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUIMEDICAM', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis del producto a administrar por aplicación, expresada en la unidad de medida correspondiente.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUIMEDICAM', @level2type = N'COLUMN', @level2name = N'DOSISPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUIMEDICAM', @level2type = N'COLUMN', @level2name = N'DOSISPROD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad de medida de la dosis, por ejemplo mg, mcg, mg/m², UI.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUIMEDICAM', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUIMEDICAM', @level2type = N'COLUMN', @level2name = N'CODUNIMED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Semanas del ciclo en las que se debe administrar el medicamento, según el esquema de quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUIMEDICAM', @level2type = N'COLUMN', @level2name = N'SEMANAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUIMEDICAM', @level2type = N'COLUMN', @level2name = N'SEMANAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Días específicos de administración del medicamento dentro del ciclo de quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUIMEDICAM', @level2type = N'COLUMN', @level2name = N'DIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUIMEDICAM', @level2type = N'COLUMN', @level2name = N'DIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de factor de cálculo utilizado para la dosis, por ejemplo superficie corporal, peso o dosis fija.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUIMEDICAM', @level2type = N'COLUMN', @level2name = N'TIPOFACTOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUIMEDICAM', @level2type = N'COLUMN', @level2name = N'TIPOFACTOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Instrucciones detalladas de administración del medicamento: preparación, tiempo de infusión, observaciones clínicas u otras indicaciones del profesional.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUIMEDICAM', @level2type = N'COLUMN', @level2name = N'INSTRUADMINIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUIMEDICAM', @level2type = N'COLUMN', @level2name = N'INSTRUADMINIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades o aplicaciones del medicamento que se deben administrar por día.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUIMEDICAM', @level2type = N'COLUMN', @level2name = N'CANTIDADXDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUIMEDICAM', @level2type = N'COLUMN', @level2name = N'CANTIDADXDIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad total de unidades o aplicaciones del medicamento durante todo el ciclo de quimioterapia.', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUIMEDICAM', @level2type = N'COLUMN', @level2name = N'CANTIDADXCICLO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'HCQUIMEDICAM', @level2type = N'COLUMN', @level2name = N'CANTIDADXCICLO';
