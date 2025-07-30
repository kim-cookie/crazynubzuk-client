using System;
using System.Collections.Generic; 

public abstract record ResponsePacketData
{
    // PONG
    public sealed record Pong() : ResponsePacketData;
    // 로비 입장
    public sealed record EnterLobby() : ResponsePacketData;
    // 방 생성
    public sealed record CreateRoom(
        string roomName,
        int participants,
        string hostName
        // 필요한 필드가 있다면 여기 추가
    ) : ResponsePacketData;
    // 방 입장
    public sealed record EnterRoom(string direction) : ResponsePacketData;
    // 게임 준비
    public sealed record ReadyGame(bool hostReady, bool guestReady) : ResponsePacketData;
    // 게임 시작
    public sealed record StartGame() : ResponsePacketData;
    // 재시작
    public sealed record Restart() : ResponsePacketData;
    // 나가기 (로비)
    public sealed record Exit() : ResponsePacketData;
    // 기록
    public sealed record Record(string roomId, List<RankingData> rankingList) : ResponsePacketData;

    public record RankingData(int rank, string username1, string username2, int highest);
    // 캐릭터 Balance
    public sealed record UpdateBalance(float balance, float survivalTime, bool isGameOver) : ResponsePacketData;
}


// public abstract record ResponsePacketData
// {
//     // PONG
//     public sealed record Pong() : ResponsePacketData;

//     // 방 입장 응답
//     public sealed record EnterRoom(bool success, int participantCount) : ResponsePacketData;

//     // 방 퇴장 응답
//     public sealed record LeaveRoom(bool success) : ResponsePacketData;

//     // 방 생성 응답
//     public sealed record CreateRoom(bool success) : ResponsePacketData;

//     // 방 인원 변화
//     public sealed record PlayerCountChanged(int participantCount, int maxPlayer) : ResponsePacketData;

//     // 방장 여부
//     public sealed record YouAreHost(bool isHost) : ResponsePacketData;

//     // 게임 준비 상태
//     public sealed record ReadyGame(bool isReady) : ResponsePacketData;

//     // 게임 시작
//     public sealed record StartGame(bool success, string message) : ResponsePacketData;

//     // 라운드 시작
//     public sealed record StartRound(string message) : ResponsePacketData;

//     // 첫 라운드 규칙
//     public sealed record FirstRoundRules(string message) : ResponsePacketData;

//     // 카드 섞기
//     public sealed record ShuffleCards(string message) : ResponsePacketData;

//     // 카드 한 장씩 분배
//     public sealed record DealOneCard(string message) : ResponsePacketData;

//     // 내 카드 정보
//     public sealed record YourCard(int cardNumber, string cardName, string message) : ResponsePacketData;

//     // 내 순위
//     public sealed record YourRank(string rank, string message) : ResponsePacketData;

//     // 내 차례 순서
//     public sealed record YourOrder(int order, string message) : ResponsePacketData;

//     // 다음 페이지 UI
//     public sealed record NextPage(bool success) : ResponsePacketData;

//     // 라운드 시작 알림
//     public sealed record RoundStarted(bool success, string message, string nickname) : ResponsePacketData;

//     // 카드 분배
//     public sealed record DealCards(string message) : ResponsePacketData;

//     // 교환 단계
//     public sealed record ExchangePhase(string message) : ResponsePacketData;

//     // 교환 정보
//     public sealed record ExchangeInfo(string message) : ResponsePacketData;

//     // 교환 정보 2
//     public sealed record ExchangeInfo2(bool nubjukOrLkh) : ResponsePacketData;

//     // 교환 완료
//     public sealed record ExchangeDone(string message) : ResponsePacketData;

//     // 내 턴 알림
//     public sealed record YourTurn(string message) : ResponsePacketData;

//     // 카드 더미 업데이트
//     public sealed record PileUpdate(string playerId, List<int> cards) : ResponsePacketData;

//     // 패스 여부
//     public sealed record HasPassed(string message) : ResponsePacketData;

//     // 모두 패스
//     public sealed record AllPassed(string message) : ResponsePacketData;

//     // 턴 종료
//     public sealed record EndTurn(bool isTurnOver) : ResponsePacketData;

//     // 라운드 종료
//     public sealed record DoneRound(string nickname, string message) : ResponsePacketData;

//     // 잘못된 카드 제출
//     public sealed record InvalidCard(bool success, string message) : ResponsePacketData;

//     // 모든 정보
//     public sealed record AllInfo(List<string> nicknames, List<List<int>> hands , List<string> ranks, List<int> order) : ResponsePacketData;

//     // 패 업데이트
//     public sealed record UpdateHand(List<int> hand) : ResponsePacketData;

//     // 카드 제출 실패
//     public sealed record SubmitError(string message) : ResponsePacketData;

//     // 현재 턴
//     public sealed record CurrentTurn(string nickname) : ResponsePacketData;

//     public sealed record ShowMessage(bool success, string message) : ResponsePacketData;

// }