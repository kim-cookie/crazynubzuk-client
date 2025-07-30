using System;
using System.Collections.Generic;

public abstract record RequestPacketData
{
    // PING
    public sealed record Ping() : RequestPacketData;

    // 방 입장
    public sealed record EnterRoom(string nickname) : RequestPacketData;

    // 방 퇴장
    public sealed record LeaveRoom(string nickname) : RequestPacketData;

    // 방 정보 요청
    public sealed record GetRoomInfo(string clientId) : RequestPacketData;

    // 방 생성
    public sealed record CreateRoom(string nickname) : RequestPacketData;

    // 라운드 시작
    public sealed record RoundStarted(string nickname): RequestPacketData;

    // 게임 시작
    public sealed record StartGame() : RequestPacketData;

    // 카드 제출
    public sealed record ThrowSubmit(string nickname, List<int> cards) : RequestPacketData;

    // 카드 플레이
    public sealed record PlayCard(List<int> cards) : RequestPacketData;

    // 패스
    public sealed record Pass(string nickname) : RequestPacketData;

    // 라운드 종료
    public sealed record DoneRound(string nickname) : RequestPacketData;

    // 모든 정보 요청
    public sealed record AllInfo(string nickname) : RequestPacketData;

}