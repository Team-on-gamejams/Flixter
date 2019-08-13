using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossBase : EnemyController {
	static Vector3 leftBorder;
	static Vector3 rightBorder;
	const float stopPointY = 3.5f;

	public string BossName;

	public BossBehaviourEnums.BossMoveType moveType;

	public float AttackTime;

	bool completeMovingDown;
	bool movingLeft;
	float attackTimer;

	protected new void Awake() {
		base.Awake();

		movingLeft = Random.Range(0, 2) == 1;
		completeMovingDown = false;
		attackTimer = 0;

		EventManager.OnTimeStopChangedEvent += OnTimeStopChanged;
	}

	protected void Start() {
		leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0));
		rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0));

		ProcessMove();
	}

	protected private void OnDestroy() {
		EventManager.OnTimeStopChangedEvent -= OnTimeStopChanged;
	}

	void Update() {
		if (GameManager.Instance.IsTimeStop)
			return;

		if((attackTimer += Time.deltaTime) >= AttackTime) {
			attackTimer -= AttackTime;
			ProcessAttack();
		}
	}

	protected abstract void ProcessAttack();

	void OnTimeStopChanged(EventData data) {
		if (GameManager.Instance.IsTimeStop)
			LeanTween.cancel(gameObject);
		else
			ProcessMove();
	}

	//TODO: cool effect when boss appear
	//TODO: boss moving down when you speed up
	public void ProcessMove() {
		if (!completeMovingDown) {
			LeanTween.moveLocalY(gameObject, stopPointY, (transform.position.y - stopPointY) / speed)
			.setOnComplete(() => {
				completeMovingDown = true;
				ProcessMove();
			});
		}
		else {
			switch (moveType) {
				case BossBehaviourEnums.BossMoveType.LeftRight:
					if (movingLeft) {
						LeanTween.moveLocalX(gameObject, leftBorder.x, Mathf.Abs((transform.position.x - leftBorder.x)) / speed)
						.setOnComplete(() => {
							movingLeft = !movingLeft;
							ProcessMove();
						});
					}
					else {
						LeanTween.moveLocalX(gameObject, rightBorder.x, Mathf.Abs((transform.position.x - rightBorder.x)) / speed)
						.setOnComplete(() => {
							movingLeft = !movingLeft;
							ProcessMove();
						});
					}
					break;

				case BossBehaviourEnums.BossMoveType.FollowPlayer:
				case BossBehaviourEnums.BossMoveType.FollowPlayerRealtime: //TODO: change to realy realtime
					float playerX = GameManager.Instance.Player.player.gameObject.transform.position.x;
					LeanTween.moveLocalX(gameObject, playerX, Mathf.Abs((transform.position.x - playerX)) / speed)
					.setOnComplete(ProcessMove);
					break;
			}
		}
	}

	public string GetBossName() => BossName;
}
