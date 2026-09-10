Partial Public Class ScheduleDetailConcept

    Public Overloads Shared Widening Operator CType(ByVal detailConcept As ScheduleDetailConcept) As NoveltyScheduleDetailConcept
        Dim deduccionConcepto As New NoveltyScheduleDetailConcept()
        deduccionConcepto.ConceptType = detailConcept.ConceptType
        deduccionConcepto.ConceptId = detailConcept.ConceptId
        Return deduccionConcepto
    End Operator

End Class
