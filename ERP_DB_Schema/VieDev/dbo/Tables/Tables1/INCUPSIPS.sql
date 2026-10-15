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
ALTER TABLE [dbo].[INCUPSIPS] NOCHECK CONSTRAINT [FK_INCUPSIPS_INCONFACT];


GO
ALTER TABLE [dbo].[INCUPSIPS] NOCHECK CONSTRAINT [FK_INCUPSIPS_INCUPSSUB];


GO
ALTER TABLE [dbo].[INCUPSIPS] NOCHECK CONSTRAINT [FK_INCUPSIPS_INDIAGNOS];


GO
ALTER TABLE [dbo].[INCUPSIPS] NOCHECK CONSTRAINT [FK_INCUPSIPS_INPORCIVA];


GO
CREATE NONCLUSTERED INDEX [IX_INCUPSIPS_SIPSESTADO_TIPSERIPS]
    ON [dbo].[INCUPSIPS]([SIPSESTADO] ASC, [TIPSERIPS] ASC)
    INCLUDE([DESSERIPS], [IPSSERIAD], [OXIGENSERVICE], [RequiresLaterality], [SERIPSDASH], [SERIPSPOS], [SERREASIT], [TIPSERTER]);


GO
CREATE NONCLUSTERED INDEX [IX_INCUPSIPS_CODGRUSUB_DESSERIPS]
    ON [dbo].[INCUPSIPS]([CODGRUSUB] ASC)
    INCLUDE([DESSERIPS]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para guardar los servicios de apoyo imagenológico a procedimientos ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'ImageGuidanceProcedure';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Bandera para determinar si se lista servicio en el dashboard    1: Laboratorios  2: Patologias  3: Imagenes Diagnosticas  4. Consulta Externa  5. Quimioterapias  6. Radioterapias  7. Diálisis  8. Ninguno  9. Procedimiento no Qx  10. Procedimiento Qx  11. Interconsultas  12. Otros Procedimientos  13. Braquiterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'SERIPSDASHAMBU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si un procedimiento QX es mandatorio realizar el informe QX, por defecto este campo esta en SI.  La afectacion que tiene es que cuando se vaya a egresar un paciente, el sistema cambiara a realizado todos los procedimientos QX que esten pendiente de confirmar. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'MandatoryQxReport';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Propiedad que indica si el CUP requiere lateralidad (true or false).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'RequiresLaterality';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Requiere Sala  0 - No requiere sala', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'SOLISALA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Servicio de oxigeno:   True=Si   False=No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'OXIGENSERVICE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '"01", "Consultas (AC)"  "02", "Procedimientos de diagnósticos (AP)"  "03", "Procedimientos terapéuticos no quirúrgicos (AT)"  "04", "Procedimientos terapéuticos quirúrgicos (AP)"  "05", "Procedimientos de promoción y prevención (AP)"  "06", "Estancias (AT)"  "07", "Honorarios (AT)"  "08", "Derechos de sala (AT)"  "09", "Materiales e insumos (AT)"  "10", "Banco de sangre (AP)"  "11", "Prótesis y órtesis (AT)"  "12", "Medicamentos POS (AT)"  "13", "Medicamentos no POS (AT)"  "14", "Traslado de pacientes (AT)"', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'CONCEPTORIPSNORIAS';


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Liquidacion  1:ISS 2001;  2:SOAT;  3:ISS 2004;  4:CUPS;', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'TLQSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'POS   1 = si es pbs    0 = No es pbs', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCUPSIPS', @level2type = N'COLUMN', @level2name = N'SERIPSPOS';


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

