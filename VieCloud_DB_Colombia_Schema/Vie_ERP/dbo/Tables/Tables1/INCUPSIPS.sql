CREATE TABLE [dbo].[INCUPSIPS] (
    [CODSERIPS]              CHAR (20)     NOT NULL,
    [DESSERIPS]              CHAR (300)    NOT NULL,
    [CODGRUIPS]              CHAR (3)      NULL,
    [CODSUBIPS]              CHAR (10)     NULL,
    [CODGRUSUB]              CHAR (13)     NOT NULL,
    [NIVSERIPS]              INT           NOT NULL,
    [TIPSERIPS]              INT           NULL,
    [TIPSERTER]              BIT           NULL,
    [SERREASIT]              BIT           NULL,
    [SERPERINF]              BIT           NULL,
    [PRESERIPS]              CHAR (1)      NULL,
    [CLASERIPS]              CHAR (1)      NULL,
    [PROSERIPS]              CHAR (1)      NULL,
    [ARSCODIGO]              CHAR (10)     NULL,
    [CODCONCEP]              CHAR (3)      NULL,
    [SEMANCOTI]              CHAR (3)      NULL,
    [EDADMAXI]               CHAR (3)      NULL,
    [UNIEDAMAX]              CHAR (1)      NULL,
    [EDADMINI]               CHAR (3)      NULL,
    [UNIEDADMI]              CHAR (1)      NULL,
    [SEXOSERIPS]             CHAR (1)      NULL,
    [SERIPSEXP]              TINYINT       NULL,
    [SERIPSPOS]              BIT           NULL,
    [PARTABOR]               BIT           NULL,
    [PERUTISER]              CHAR (3)      NULL,
    [NUMAUTSER]              CHAR (3)      NULL,
    [CANMAXSIPS]             CHAR (3)      NULL,
    [FORFARIPS]              VARCHAR (300) NULL,
    [UNIFORFAR]              VARCHAR (100) NULL,
    [CODDIAGNO]              CHAR (4)      NULL,
    [SRIPSCONC]              VARCHAR (80)  NULL,
    [SIPSCUMP]               BIT           NULL,
    [SIPSCCO]                BIT           NULL,
    [CODGOCUPS]              CHAR (20)     NULL,
    [DESCODCUPS]             VARCHAR (300) NULL,
    [CODSUBATE]              CHAR (2)      NULL,
    [CODGOIVA]               CHAR (2)      NULL,
    [IPSASTROS]              BIT           NULL,
    [IPSPATOLO]              BIT           NULL,
    [IPSREHTRA]              BIT           NULL,
    [IPSPROPRE]              BIT           NULL,
    [IPSSCITOTE]             BIT           NULL,
    [IPSAFEEFRS]             BIT           NULL,
    [IPSMACCOS]              BIT           NULL,
    [SIPSESTADO]             BIT           NULL,
    [DESPROPRE]              VARCHAR (300) NULL,
    [TIPOSEIPS]              CHAR (1)      NULL,
    [NIVELSIPS]              CHAR (1)      NULL,
    [TLQSERIPS]              CHAR (1)      NULL,
    [IPSSERIAD]              BIT           NULL,
    [SERIPSDASH]             INT           NULL,
    [TIPCONNUT]              BIT           NULL,
    [TIPCONPSI]              BIT           NULL,
    [TIPCONJOV]              BIT           NULL,
    [TIPCONADU]              BIT           NULL,
    [TIPASEPRE]              BIT           NULL,
    [TIPASEPOS]              BIT           NULL,
    [TIPTSHNEO]              BIT           NULL,
    [TIPANTHEP]              BIT           NULL,
    [TIPSERSIF]              BIT           NULL,
    [TIPELIVIH]              BIT           NULL,
    [TIPHEMOGL]              BIT           NULL,
    [TIPGLIBASA]             BIT           NULL,
    [TIPCREATI]              BIT           NULL,
    [TIPHEMGLI]              BIT           NULL,
    [TIPMICROA]              BIT           NULL,
    [TIPHDL]                 BIT           NULL,
    [TIPBACDIA]              BIT           NULL,
    [TIPCPPRIM]              BIT           NULL,
    [TIPCPRENA]              BIT           NULL,
    [TIPVALVIS]              BIT           NULL,
    [TIPCONOFT]              BIT           NULL,
    [TIPCONDES]              BIT           NULL,
    [TIPPLAFAM]              BIT           NULL,
    [TIPMAMOG]               BIT           NULL,
    [TIPBIOCER]              BIT           NULL,
    [TIPBIOSEN]              BIT           NULL,
    [CREATINURIA]            BIT           NULL,
    [COLETOTAL]              BIT           NULL,
    [LDL]                    BIT           NULL,
    [PTH]                    BIT           NULL,
    [ALBUMSERICA]            BIT           NULL,
    [ALBUMFOSFO]             BIT           NULL,
    [EXIGEINTERPRE]          BIT           NULL,
    [EXIGECONFIRM]           BIT           NULL,
    [PARESCAPA]              BIT           NULL,
    [APLICARIAS]             BIT           NULL,
    [CONCEPTORIPSNORIAS]     VARCHAR (2)   NULL,
    [OXIGENSERVICE]          BIT           CONSTRAINT [DF_INCUPSIPS_OXIGENSERVICE] DEFAULT ((0)) NOT NULL,
    [SOLISALA]               BIT           NULL,
    [ISPANEL]                TINYINT       CONSTRAINT [DF_INCUPSIPS_ISPANEL] DEFAULT ((0)) NULL,
    [RequiresLaterality]     BIT           NULL,
    [MandatoryQxReport]      BIT           CONSTRAINT [DF_INCUPSIPS_MandatoryQxReport] DEFAULT ((1)) NOT NULL,
    [SERIPSDASHAMBU]         INT           NULL,
    [ImageGuidanceProcedure] BIT           CONSTRAINT [DF_INCUPSIPS_ImageGuidanceProcedure] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_INCUPSIPS] PRIMARY KEY CLUSTERED ([CODSERIPS] ASC),
    CONSTRAINT [FK_INCUPSIPS_INCONFACT] FOREIGN KEY ([CODCONCEP]) REFERENCES [dbo].[INCONFACT] ([CODCONCEP]),
    CONSTRAINT [FK_INCUPSIPS_INCUPSSUB] FOREIGN KEY ([CODGRUSUB]) REFERENCES [dbo].[INCUPSSUB] ([CODGRUSUB]) ON UPDATE CASCADE,
    CONSTRAINT [FK_INCUPSIPS_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_INCUPSIPS_INPORCIVA] FOREIGN KEY ([CODGOIVA]) REFERENCES [dbo].[INPORCIVA] ([CODGOIVA])
);




GO



GO



GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_INCUPSIPS_SIPSESTADO_TIPSERIPS]
    ON [dbo].[INCUPSIPS]([SIPSESTADO] ASC, [TIPSERIPS] ASC)
    INCLUDE([DESSERIPS], [IPSSERIAD], [OXIGENSERVICE], [RequiresLaterality], [SERIPSDASH], [SERIPSPOS], [SERREASIT], [TIPSERTER]);


GO
CREATE NONCLUSTERED INDEX [IX_INCUPSIPS_SIPSESTADO_TIPSERIPS_INC_DESSERIPS_IPSSERIAD_OXIGENSERVICE_SERIPSDASH_SERIPSPOS_SERREASIT_TIPSERTER]
    ON [dbo].[INCUPSIPS]([SIPSESTADO] ASC, [TIPSERIPS] ASC)
    INCLUDE([DESSERIPS], [IPSSERIAD], [OXIGENSERVICE], [SERIPSDASH], [SERIPSPOS], [SERREASIT], [TIPSERTER]);


GO
CREATE NONCLUSTERED INDEX [IX_INCUPSIPS_CODGRUSUB_DESSERIPS]
    ON [dbo].[INCUPSIPS]([CODGRUSUB] ASC)
    INCLUDE([DESSERIPS]);


GO
CREATE NONCLUSTERED INDEX [IX_INCUPSIPS__CODSERIPS__DESSERIPS]
    ON [dbo].[INCUPSIPS]([CODSERIPS] ASC, [DESSERIPS] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para guardar los servicios de apoyo imagenológico a procedimientos ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'ImageGuidanceProcedure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Bandera para determinar si se lista servicio en el dashboard    1: Laboratorios  2: Patologias  3: Imagenes Diagnosticas  4. Consulta Externa  5. Quimioterapias  6. Radioterapias  7. Diálisis  8. Ninguno  9. Procedimiento no Qx  10. Procedimiento Qx  11. Interconsultas  12. Otros Procedimientos  13. Braquiterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'SERIPSDASHAMBU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si un procedimiento QX es mandatorio realizar el informe QX, por defecto este campo esta en SI.  La afectacion que tiene es que cuando se vaya a egresar un paciente, el sistema cambiara a realizado todos los procedimientos QX que esten pendiente de confirmar. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'MandatoryQxReport';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Propiedad que indica si el CUP requiere lateralidad (true or false).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'RequiresLaterality';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si el cupstiene panel', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'ISPANEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Requiere Sala  0 - No requiere sala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'SOLISALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Servicio de oxigeno:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'OXIGENSERVICE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '"01", "Consultas (AC)"  "02", "Procedimientos de diagnósticos (AP)"  "03", "Procedimientos terapéuticos no quirúrgicos (AT)"  "04", "Procedimientos terapéuticos quirúrgicos (AP)"  "05", "Procedimientos de promoción y prevención (AP)"  "06", "Estancias (AT)"  "07", "Honorarios (AT)"  "08", "Derechos de sala (AT)"  "09", "Materiales e insumos (AT)"  "10", "Banco de sangre (AP)"  "11", "Prótesis y órtesis (AT)"  "12", "Medicamentos POS (AT)"  "13", "Medicamentos no POS (AT)"  "14", "Traslado de pacientes (AT)"', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'CONCEPTORIPSNORIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si el cups Aplica o no para RIAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'APLICARIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(No se encontró documentación de este campo en la solución de crystal ni información por parte de los desarrolladores.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'PARESCAPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Exige Confirmación- Procedimientos NO QX', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'EXIGECONFIRM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Exige Interpretacion - Procedimientos NO QX', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'EXIGEINTERPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 2463:   Albumina Fósforo  1-Si  0-No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'ALBUMFOSFO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 2463:  Albumina Sérica  1-Si  0-No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'ALBUMSERICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 2463:  PTH  1-Si  0-No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'PTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 2463:  LDL  1-Si  0-No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'LDL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 2463:  Colesterol Total  1-Si  0-No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'COLETOTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 2463:  Creatinuria  1-Si  0-No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'CREATINURIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 4505:   Biopsia Seno por Bacaf: 1-Si  0-No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'TIPBIOSEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 4505:   Biopsia Cervical: 1-Si  0-No  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'TIPBIOCER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 4505:  Tipo de servicio mamografía: 1-Si  0-No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'TIPMAMOG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 4505:  Planificación Familiar Primera vez: 1-Si  0-No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'TIPPLAFAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 4505:  Consulta Crecimiento y desarrollo Primera Vez: 1-Si  0-No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'TIPCONDES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 4505:  Consulta por oftalmología: 1-Si  0-No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'TIPCONOFT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 4505:  Valoración de la Agudeza Visual: 1-Si  0-No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'TIPVALVIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 4505:  Control Prenatal: 1-Si  0-No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'TIPCPRENA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 4505:  Control Prenatal Primera Vez: 1-Si  0-No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'TIPCPPRIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 4505:  Baciloscopia de Diagnóstico: 1-Si  0-No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'TIPBACDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 4505:  HDL : 1-Si  0-No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'TIPHDL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 4505:  Microalbuminuria : 1-Si  0-No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'TIPMICROA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 4505:  Hemoglobina Glicosilada: 1-Si  0-No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'TIPHEMGLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 4505:  Creatinina : 1-Si  0-No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'TIPCREATI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 4505:  Glisemia Basal: 1-Si  0-No  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'TIPGLIBASA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 4505:  Hemoglobina : 1-Si  0-No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'TIPHEMOGL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 4505:  Elisa para VIH: 1-Si  0-No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'TIPELIVIH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 4505:  Serología para Sífilis: 1-Si  0-No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'TIPSERSIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 4505:  Antígeno de Superficie Hepatitis B en Gestantes: 1-Si  0-No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'TIPANTHEP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 4505:  TSH Neonatal: 1-Si  0-No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'TIPTSHNEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 4505:  Asesoría Pos test Elisa para VIH: 1-Si  0-No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'TIPASEPOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 4505:  Asesoría Pre test Elisa para VIH: 1-Si  0-No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'TIPASEPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 4505:  Consulta de Adulto Primera Vez: 1-Si  0-No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'TIPCONADU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 4505:  Consulta de Joven Primera Vez: 1-Si  0-No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'TIPCONJOV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 4505:  Consulta de Psicologia: 1-Si  0-No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'TIPCONPSI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo 4505:  Consulta de Nutrición: 1-Si  0-No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'TIPCONNUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Bandera para determinar si se lista servicio en el dashboard    1: Laboratorios  2: Patologias  3: Imagenes Diagnosticas  4. Consulta Externa  5. Quimioterapias  6. Radioterapias  7. Diálisis  8. Ninguno  9. Procedimiento no Qx  10. Procedimiento Qx  11. Interconsultas  12. Otros Procedimientos  13. Braquiterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'SERIPSDASH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Servicio seriado si:1 no:0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'IPSSERIAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Liquidacion  1:ISS 2001;  2:SOAT;  3:ISS 2004;  4:CUPS;', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'TLQSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nivel', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'NIVELSIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'TIPOSEIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de las actividades del servicio de promocion y prevencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'DESPROPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Servicio IPS 1=Activo;0=Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'SIPSESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modificar area y centro de costo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'IPSMACCOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Afecta EFRS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'IPSAFEEFRS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Servicio de citologia toma y entrega', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'IPSSCITOTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Opcion Promocion y Prevencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'IPSPROPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Recalcular en Hoja de Trabajo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'IPSREHTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Opcion Patologia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'IPSPATOLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Opcion Astroscopia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'IPSASTROS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo IVA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'CODGOIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo SubAtencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'CODSUBATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Codigo CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'DESCODCUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'CODGOCUPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Control de Cantidades por Ordenes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'SIPSCCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Controlar Utilizacion Maxima por Periodo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'SIPSCUMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concentracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'SRIPSCONC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Diagnostico Principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de la Forma Farmaceutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'UNIFORFAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Forma Farmaceutica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'FORFARIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Maxima del Servicio por Orden', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'CANMAXSIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' Numero de Veces que se Puede Autorizar el Servicio en el Periodo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'NUMAUTSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Perdiodo de Utilizacion del Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'PERUTISER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parto / Aborto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'PARTABOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'POS   1 = si es pbs    0 = No es pbs', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'SERIPSPOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dias de Expiracion del Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'SERIPSEXP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sexo que cubren los servicios 0:Masculino;1:Femenino', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'SEXOSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Edad Minima ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'UNIEDADMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad Minima que cubre el servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'EDADMINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Edad Maxima ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'UNIEDAMAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad Maxima que Cubre el Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'EDADMAXI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Semanas Cotizadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'SEMANCOTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Concepto de facturacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'CODCONCEP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Area de Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'ARSCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'OBTIENE O ESTABLECE EL SERVICIO DE:  (DIAGNOSTICO = 1,LABORATORIO = 2,ODONTOLOGIA = 3,CONSULTA_URGENCIAS = 4,HOSPITALIZACION = 5)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'PROSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = ' CLASE DE SERVICIO (NINGUNO = 1,CIRUJANO = 2,ANESTESIOLOGO = 3,AYUDANTE = 4,DERECHO_SALA = 5,MATERIALES_SUTURA = 6,INSTRUMENTACION_QUIRURGICA = 7)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'CLASERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TIPO DE SERVICIO (NO_QUIRURGICO = 1,QUIRURGICO = 2,PAQUETE = 3)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'PRESERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permitir Diligenciar Informe para la Realización de Procedimientos Qx Menores', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'SERPERINF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Servicio se Realiza en Sitio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'SERREASIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si el procedimiento es de Terapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'TIPSERTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de Servicio   
1: Laboratorios  
2: Patologias  
3: Imagenes Diagnosticas  
4: Procedimeintos no Qx  
5: Procedimientos Qx  
6: Interconsultas  
7:Ninguno  
8:Consulta Externa  
9:Hemocomponentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'TIPSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nivel de Complejidad del Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'NIVSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Grupos y Subgrupos CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'CODGRUSUB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del SubGrupo del Procedimiento o Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'CODSUBIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Grupo del Procedimiento o Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'CODGRUIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Procedimiento o Servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'DESSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla maestra del catálogo de servicios CUPS/IPS habilitados en el sistema de salud colombiano. Almacena cada servicio con su código, descripción, clasificación por grupo/subgrupo, nivel de atención, tipo de servicio, restricciones de edad y sexo, y atributos clínicos como si es de promoción/prevención, requiere lateralidad, exige interpretación o confirmación, y si aplica para RIAS. Referencia diagnósticos CIE-10, subgrupos CUPS, conceptos de facturación RIPS e IVA, permitiendo su uso en facturación, agendamiento y reportes regulatorios.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'INCUPSIPS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'backport_from_v25.47c_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'INCUPSIPS';
GO

GO
CREATE NONCLUSTERED INDEX [IX_INCUPSIPS_CODSERIPS]
    ON [dbo].[INCUPSIPS]([CODSERIPS] ASC);
