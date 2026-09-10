Imports System.Runtime.Serialization
Imports Infrastructure.CrossCutting.Base

<DataContract(IsReference:=True), Serializable(), KnownType(GetType(Homologation))> _
<KnownType(GetType(ServiceOrderDetail))> _
<KnownType(GetType(CupsHomologation))> _
Public Class Homologation

    <DataMember()>
    Public Service As ServiceOrderDetail


    <DataMember()>
    Public Homologations As List(Of CupsHomologation)


    Public Function ToXml() As String
        'Dim HomologationsXml As String = "<CupsHomologation>
        '                <GuidHomologation>{0}</GuidHomologation>
        '                <CupsHomologationId>{1}</CupsHomologationId>
        '                <IPSServiceId>{2}</IPSServiceId>
        '                <CupsEntityId>{3}</CupsEntityId>
        '                <CodeNameCupsEntity>{4}</CodeNameCupsEntity>
        '                <CodeNameIpsService>{5}</CodeNameIpsService>
        '                <Activated>{6}</Activated>
        '            </CupsHomologation>"
        Dim d = String.Join(vbCrLf, Homologations.Select(Function(m)
                                                             Return $"<CupsHomologation>
                                                                {If(Not String.IsNullOrEmpty(m.GuidHomologation), $"<GuidHomologation>{m.GuidHomologation}</GuidHomologation>", Nothing)}
                                                                <CupsHomologationId>{m.Id}</CupsHomologationId>
                                                                <IPSServiceId>{m.IPSServiceId}</IPSServiceId>
                                                                <CupsEntityId>{m.CupsEntityId}</CupsEntityId>
                                                                <CodeNameCupsEntity>{m.CodeNameCupsEntity.ConvertToXmlText()}</CodeNameCupsEntity>
                                                                <CodeNameIpsService>{m.CodeNameIpsService.ConvertToXmlText()}</CodeNameIpsService>
                                                                <Activated>{If(m.Activated, 1, 0)}</Activated>
                                                            </CupsHomologation>"
                                                             'Return String.Format(
                                                             '   HomologationsXml,
                                                             '   m.GuidHomologation,
                                                             '   m.Id,
                                                             '   m.IPSServiceId,
                                                             '   m.CupsEntityId,
                                                             '   m.CodeNameCupsEntity,
                                                             '   m.CodeNameIpsService,
                                                             '   If(m.Activated, 1, 0)
                                                             '  )
                                                         End Function).ToArray())
        Return $"
<Homologacion>
    <Service>
        <Id>{Service.ServiceOrderDetailDistribution(0).Id}</Id>
        <CUPSAssociateService>{If(Service.CUPSAssociateService, 1, 0)}</CUPSAssociateService>
        <ServiceOrderDetailId>{Service.Id}</ServiceOrderDetailId>
        <IsDelete>{If(Service.IsDelete, 1, 0)}</IsDelete>
        <IsFirstEvent>{If(Service.IsFirstEvent, 1, 0)}</IsFirstEvent>
        <DistributionType>{Service.ServiceOrderDetailDistribution(0).DistributionType}</DistributionType>
        <LastCaregroupId>{Service.ServiceOrderDetailDistribution(0).LastCaregroupId}</LastCaregroupId>
        <RecordType>{Service.RecordType}</RecordType>
        {If(Service.CUPSEntityId IsNot Nothing, $"<CUPSEntityId>{Service.CUPSEntityId.Value}</CUPSEntityId>", Nothing)}
        <PerformsFunctionalUnitId>{Service.PerformsFunctionalUnitId}</PerformsFunctionalUnitId>
        {If(Service.PerformsProfessionalSpecialty IsNot Nothing, $"<PerformsProfessionalSpecialty>{Service.PerformsProfessionalSpecialty}</PerformsProfessionalSpecialty>", Nothing)}
        <ServiceDate>{Service.ServiceDate.ToString("dd/MM/yyyy HH:mm:ss")}</ServiceDate>
        {If(Service.IPSServiceId IsNot Nothing, $"<IPSServiceId>{Service.IPSServiceId.Value}</IPSServiceId>", Nothing)}
        {If(Service.CodeNameSpeciality IsNot Nothing, $"<CodeNameSpeciality>{Service.CodeNameSpeciality}</CodeNameSpeciality>", Nothing)}
        {If(Service.CodeNameFunctionalUnit IsNot Nothing, $"<CodeNameFunctionalUnit>{Service.CodeNameFunctionalUnit}</CodeNameFunctionalUnit>", Nothing)}
        {If(Service.CodeNameHealthAdministrator IsNot Nothing, $"<CodeNameHealthAdministrator>{Service.CodeNameHealthAdministrator}</CodeNameHealthAdministrator>", Nothing)}
        {If(Service.ThirdPartyId IsNot Nothing, $"<ThirdPartyId>{Service.ThirdPartyId.Value}</ThirdPartyId>", Nothing)}        
    </Service>
    {If(d IsNot Nothing, $"<Homologations>{d}</Homologations>", Nothing)}
</Homologacion>
"
    End Function

End Class
