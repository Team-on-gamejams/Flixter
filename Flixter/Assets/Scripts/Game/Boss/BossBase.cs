using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase : EnemyController {
	public BossBehaviourEnums.BossMoveType moveType;

	const float stopPoint = 3.5f;

	protected new void Awake() {
		base.Awake();
		EventManager.OnTimeStopChangedEvent += OnTimeStopChanged;
	}

	protected void Start() {
		MoveToStartPos();
	}

	protected private void OnDestroy() {
		EventManager.OnTimeStopChangedEvent -= OnTimeStopChanged;
	}

	void OnTimeStopChanged(EventData data) {
		if (GameManager.Instance.IsTimeStop)
			LeanTween.cancel(gameObject);
		else
			MoveToStartPos();
	}

	void MoveToStartPos() {
		LeanTween.moveLocalY(gameObject, stopPoint, (transform.position.y - stopPoint) / speed)
		.setOnComplete(() => {
			switch (moveType) {
				case BossBehaviourEnums.BossMoveType.LeftRight:
					break;
				case BossBehaviourEnums.BossMoveType.FollowPlayer:
					break;
			}
		});
	}

	public virtual string GetBossName() => "NO BOSS NAME";
}
