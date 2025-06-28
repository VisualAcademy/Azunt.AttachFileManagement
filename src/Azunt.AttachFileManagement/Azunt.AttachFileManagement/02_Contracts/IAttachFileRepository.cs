/*
    작성자: 박용준 (https://dul.me/about)
    타이틀: 닷넷코리아 - 첨부 파일 관리자 리포지토리 인터페이스
    설명: AttachFiles 테이블과 연동되는 리포지토리 인터페이스입니다.
    작성일: 2017-12-08
    수정일:
        - 2021-11-12: .NET Standard 프로젝트로 이동
        - 2025-06-28: Azunt 네임스페이스 및 NuGet 패키지용 리팩터링
*/

using System.Collections.Generic;

namespace Azunt.AttachFileManagement
{
    /// <summary>
    /// 첨부 파일 데이터에 접근하는 리포지토리 인터페이스입니다.
    /// </summary>
    public interface IAttachFileRepository
    {
        /// <summary>
        /// 새 첨부 파일을 추가합니다.
        /// </summary>
        void Add(AttachFileModel model);

        /// <summary>
        /// 전체 첨부 파일 목록을 페이징하여 반환합니다.
        /// </summary>
        List<AttachFileModel> GetAll(int pageNumber = 1, int pageSize = 10);

        /// <summary>
        /// 특정 게시판과 게시글에 속한 첨부 파일 목록을 반환합니다.
        /// </summary>
        List<AttachFileModel> GetByBoardAndArticle(int boardId, int articleId);

        /// <summary>
        /// 고유 ID를 기준으로 첨부 파일 정보를 반환합니다.
        /// </summary>
        AttachFileModel GetById(int id);

        /// <summary>
        /// 파일명을 기준으로 첨부 파일 목록을 검색합니다.
        /// </summary>
        List<AttachFileModel> GetByFileName(string fileName);

        /// <summary>
        /// 파일 정보를 수정합니다.
        /// </summary>
        void UpdateById(string fileName, int fileSize, int id);

        /// <summary>
        /// 다운로드 횟수를 1 증가시킵니다.
        /// </summary>
        void UpdateDownCountById(int id);

        /// <summary>
        /// 고유 ID를 기준으로 첨부 파일을 삭제합니다.
        /// </summary>
        void RemoveById(int id);
    }
}
