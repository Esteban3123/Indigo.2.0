CREATE TABLE [MedicalHistory].[NursingPackagesOrder] (
    [Id]           INT          IDENTITY (1, 1) NOT NULL,
    [IDHCFARMEPC]  NUMERIC (18) NOT NULL,
    [IDAGPAQUETES] INT          NOT NULL,
    [NUMINGRES]    CHAR (10)    NOT NULL,
    [CODCENATE]    CHAR (10)    NOT NULL,
    [UFUCODIGO]    CHAR (10)    NOT NULL,
    [Status]       INT          NOT NULL,
    [CreationDate] DATETIME     NOT NULL,
    [CreationUser] CHAR (20)    NOT NULL,
    CONSTRAINT [PK_HcfarmepcNursingPackages] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_NursingPackagesOrder_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_NursingPackagesOrder_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_NursingPackagesOrder_HCFARMEPC] FOREIGN KEY ([IDHCFARMEPC]) REFERENCES [dbo].[HCFARMEPC] ([CODCONCEC]),
    CONSTRAINT [FK_NursingPackagesOrder_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);




GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el registro (CHAR 20). Identificación del profesional/operario que registró la orden de enfermería.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrder', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el usuario de creación', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrder', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrder', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación (DATETIME). Marca temporal del registro de la orden de paquetes.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrder', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda la fecha de creación', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrder', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrder', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de la orden: 1=Solicitado, 2=Dispensado, 3=Legalizado. Rastrea el ciclo de vida del paquete de enfermería.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrder', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del paquete:  1: Solicitado   2: Dispensado  3: Legalizado', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrder', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrder', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de unidad funcional (FK → INUNIFUNC.UFUCODIGO). Área clínica/departamento responsable de la orden enfermería.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrder', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrder', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrder', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención/establecimiento de salud (FK → ADCENATEN.CODCENATE). Identifica dónde se dispensa el paquete.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrder', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el Código centro de atención', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrder', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrder', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso/atención (FK → ADINGRESO.NUMINGRES). Vincula la orden de paquetes al episodio de atención del paciente.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrder', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el numero de ingreso', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrder', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrder', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del paquete de enfermería (FK → AGPAQUETES.ID). Referencia al catálogo de paquetes de prestaciones enfermería.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrder', @level2type = N'COLUMN', @level2name = N'IDAGPAQUETES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID del paquetes de enfermeria', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrder', @level2type = N'COLUMN', @level2name = N'IDAGPAQUETES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrder', @level2type = N'COLUMN', @level2name = N'IDAGPAQUETES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo de dispensación farmacéutica (FK → HCFARMEPC.CODCONCEC). Se registra solo para medicamentos en casa solicitados a farmacia.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrder', @level2type = N'COLUMN', @level2name = N'IDHCFARMEPC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo de la tabla de farmacia, esto solo se registra a los medicamentos que son en casa y que fueron solicitados a farmacia.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrder', @level2type = N'COLUMN', @level2name = N'IDHCFARMEPC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrder', @level2type = N'COLUMN', @level2name = N'IDHCFARMEPC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK, INT IDENTITY). Consecutivo de la orden de paquetes de enfermería.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrder', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrder', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrder', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Órdenes de paquetes de enfermería asociadas a una historia clínica y un ingreso del paciente. Registra qué paquetes de enfermería fueron ordenados, en qué centro de atención y unidad funcional, con su estado y datos de auditoría de creación.', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrder';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MedicalHistory', @level1type = N'TABLE', @level1name = N'NursingPackagesOrder';
