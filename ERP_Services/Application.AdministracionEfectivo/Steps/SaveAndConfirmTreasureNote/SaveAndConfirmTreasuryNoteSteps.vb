Imports System
Imports Domain.Base.Entities
Imports Domain.Entities
Imports TechTalk.SpecFlow

Namespace Application.AdministracionEfectivo

    <Binding()>
    Public Class SaveAndConfirmTreasuryNoteSteps
#Region "Fields"
        Private ReadOnly _TreasuryContext As TreasuryContext
        Private _TreasuryNote As New TreasuryNote
        Private _listTreasuryNoteDetail As New Domain.Entities.TrackableCollection(Of TreasuryNoteDetail)
        Private _actionResult As New ActionResult(Of TreasuryNote)
#End Region

#Region "Builder"
        Public Sub New(TreasuryContext As TreasuryContext)
            _TreasuryContext = TreasuryContext
        End Sub
#End Region
        <TechTalk.SpecFlow.Given("Genero los Maestros y Detalles")>
        Public Sub DadoGeneroLosMaestrosYDetalles()
            Dim count = 10

            For i As Int16 = 1 To count
                Dim _TreasuryNoteDetail As New TreasuryNoteDetail With
                {.NoteConceptId = 93,
                .MainAccountId = 4168,
                .ThirdPartyId = 18,
                .Nature = 1,
                .Value = 1}
                _listTreasuryNoteDetail.Add(_TreasuryNoteDetail)
            Next
            With _TreasuryNote
                .NoteDate = DateTime.Now
                .NoteType = 1
                .OperatingUnitId = 14
                .EntityBankAccountId = 2
                .MainAccountId = 3886
                .Description = "Prueba UnitTest"
                .Nature = 2
                .Value = count
                .Status = 1
                .TreasuryNoteDetail = _listTreasuryNoteDetail
            End With

        End Sub

        <TechTalk.SpecFlow.Given("Guardo y confirmo las Notas (.*)")>
        Public Sub DadoGuardoYConfirmoLasNotas(ByVal p0 As Int32)
            Dim _currentSequence As Long
            Dim sequence = _TreasuryContext._treasurySequenseAdminService.GetSequenseByIdForm("637")
            If sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                _currentSequence = sequence.TreasurySequenceDetail(0).Id
            ElseIf sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If sequence.TreasurySequenceDetail.Any(Function(S) S.IdOperatingUnit = _TreasuryNote.OperatingUnitId) Then
                    _currentSequence = sequence.TreasurySequenceDetail.Where(Function(s) s.IdOperatingUnit = _TreasuryNote.OperatingUnitId).SingleOrDefault().Id
                End If
            End If
            _actionResult = _TreasuryContext._treasuryNoteAdminService.SaveTreasuryNote(_TreasuryNote, _TreasuryContext._audit, True, _currentSequence)

            If Not _actionResult.StateResult Then
                Assert.Fail("Hubo un Error: " + _actionResult.Message)
            End If
        End Sub

        <TechTalk.SpecFlow.Then("Estas son almacenadas y Confirmadas")> _
        Public Sub EntoncesEstasSonAlmacenadasYConfirmadas()
            Assert.IsTrue(_actionResult.StateResult, _actionResult.Message)
        End Sub

    End Class

End Namespace
