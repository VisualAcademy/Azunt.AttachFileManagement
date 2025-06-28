/*
    작성자: 박용준 (https://dul.me/about) 
    타이틀: 닷넷코리아 - 첨부 파일 관리자 모델 클래스
    설명: AttachFiles 테이블과 일대일로 매핑되는 모델 클래스
    작성일: 2017-12-08
    수정일: 
        - 2021-11-12: .NET Standard 프로젝트로 이동 
        - 2025-06-28: NuGet Package로 게시
*/

using System;

namespace Azunt.AttachFileManagement
{
    /// <summary>
    /// 첨부 파일 정보를 담는 모델 클래스입니다.
    /// AttachFiles 테이블과 일대일로 매핑됩니다.
    /// </summary>
    public class AttachFileModel
    {
        /// <summary>
        /// 고유 ID (PK)
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 사용자 ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 게시판 ID
        /// </summary>
        public int BoardId { get; set; }

        /// <summary>
        /// 게시글 ID
        /// </summary>
        public int ArticleId { get; set; }

        /// <summary>
        /// 파일 이름
        /// </summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// 파일 크기 (Byte 단위)
        /// </summary>
        public int FileSize { get; set; }

        /// <summary>
        /// 다운로드 횟수
        /// </summary>
        public int DownCount { get; set; }

        /// <summary>
        /// 등록 시각
        /// </summary>
        public DateTimeOffset TimeStamp { get; set; }
    }
}
