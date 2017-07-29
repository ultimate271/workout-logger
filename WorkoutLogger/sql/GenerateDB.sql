--------------------------------------------------------------------------------
-- WLWorkoutType                                                              --
--------------------------------------------------------------------------------
create table [dbo].[WLWorkoutType](
	[workoutType_id] int not null,
	[tableName] nvarchar(128) not null,
	constraint [PK_WLWorkoutType] primary key clustered (workoutType_id asc),
	constraint [UCK_WLWorkoutType_C1] unique (tableName)
)
go

--------------------------------------------------------------------------------
-- WLWorkout                                                                  --
--------------------------------------------------------------------------------
create table [dbo].[WLWorkout](
	[workout_id] int not null,
	[workoutType_id] int not null,
	[name] nvarchar(20) not null default '',
	[comments] text not null default '',
	constraint [PK_WLWorkout] primary key clustered ([workout_id] asc),
	constraint [UCK_WlWorkout_C1] unique ([workout_id], [workoutType_id])
)
go
alter table [dbo].[WLWorkout] add constraint [FK_WLWorkout_WorkoutType] 
	foreign key ([workoutType_id]) references [dbo].[WLWorkoutType]([workoutType_id])
	on delete no action
	on update no action
go

--------------------------------------------------------------------------------
-- WLWorkoutDescriptive                                                       --
--------------------------------------------------------------------------------

create table [dbo].[WLWorkoutDescriptive](
	[workout_id] int not null,
	[workoutType_id] int not null,
	[description] text not null default '',
	constraint [PK_WLWorkoutDescriptive] primary key clustered ([workout_id] asc ,[workoutType_id]),
)
GO
insert into [dbo].[WLWorkoutType] ([workoutType_id], [tableName]) values (1, 'WLWorkoutDescriptive')
GO
alter table [dbo].[WLWorkoutDescriptive] add constraint [CHK_WLWorkoutDescriptive_Cond1] 
	check ([workoutType_id] = 1)
GO
alter table [dbo].[WLWorkoutDescriptive] add constraint [FK_WLWorkoutDescriptive_WLWorkout]
	foreign key ([workout_id], [workoutType_id]) references [dbo].[WLWorkout]([workout_id], [workoutType_id])
	on delete cascade
	on update cascade
GO

--------------------------------------------------------------------------------
-- WLMovementType                                                             --
--------------------------------------------------------------------------------
create table [dbo].[WLMovementType](
	[movementType_id] int not null,
	[tableName] nvarchar(128) not null,
	constraint [PK_WLMovementType] primary key clustered (movementType_id asc),
	constraint [UCK_WLMovementType_C1] unique (tableName)
)
GO

--------------------------------------------------------------------------------
-- WLMovement                                                                 --
--------------------------------------------------------------------------------
create table [dbo].[WLMovement](
	[movement_id] int not null,
	[movementType_id] int not null,
	[name] nvarchar(20) not null default '',
	constraint [PK_WLMovement] primary key clustered ([movement_id] asc),
	constraint [UCK_WLMovement_C1] unique ([movement_id], [movementType_id])
)
GO
alter table [dbo].[WLMovement] add constraint [FK_WLMovement_WLMovementType]
	foreign key ([movementType_id]) references [dbo].[WLMovementType]([movementType_id])
	on delete no action
	on update no action
GO

--------------------------------------------------------------------------------
-- WLMovementAir                                                              --
--------------------------------------------------------------------------------
create table [dbo].[WLMovementAir](
	[movement_id] int not null,
	[movementType_id] int not null,
	[reps] int not null default 0,
	constraint [PK_WLMovementAir] primary key clustered ([movement_id] asc, [movementType_id])
)
GO
insert into [dbo].[WLMovementType]([movementType_id], [tableName]) values (1, 'WLMovementAir')
GO
alter table [dbo].[WLMovementAir] add constraint [CHK_WLMovementAir_cond1]
	check (movementType_id = 1)
GO
alter table [dbo].[WLMovementAir] add constraint [FK_MovementAir_Movement]
	foreign key ([movement_id],[movementType_id]) references [dbo].[WLMovement] ([movement_id],[movementType_id])
	on delete cascade
	on update cascade
GO

--------------------------------------------------------------------------------
-- WLMovementWeighted                                                         --
--------------------------------------------------------------------------------
create table [dbo].[WLMovementWeighted](
	[movement_id] int not null,
	[movementType_id] int not null,
	[reps] int not null default 0,
	[weight] int not null default 0,
	constraint [PK_WLMovementWeighted] primary key clustered ([movement_id] asc, [movementType_id])
)
GO
insert into [dbo].[WLMovementType]([movementType_id], [tableName]) values (2, 'WLMovementWeighted')
GO
alter table [dbo].[WLMovementWeighted] add constraint [CHK_WLMovementWeighted_cond1]
	check (movementType_id = 2)
GO
alter table [dbo].[WLMovementWeighted] add constraint [FK_WLMovementWeighted_WLMovement]
	foreign key ([movement_id],[movementType_id]) references [dbo].[WLMovement] ([movement_id],[movementType_id])
	on delete cascade
	on update cascade
GO

--------------------------------------------------------------------------------
-- WLWorkoutTimed                                                             --
--------------------------------------------------------------------------------
create table [dbo].[WLWorkoutTimed](
	[workout_id] int not null,
	[workoutType_id] int not null,
	[roundCount] int not null default 1,
	constraint [PK_WLWorkoutTimed] primary key clustered ([workout_id], [workoutType_id])
)
GO
insert into [dbo].[WLWorkoutType] ([workoutType_id], [tableName]) values (2, 'WLWorkoutTimed')
GO
alter table [dbo].[WLWorkoutTimed] add constraint [CHK_WLWorkoutTimed_Cond1] 
	check ([workoutType_id] = 2)
GO
alter table [dbo].[WLWorkoutTimed] add constraint [FK_WLWorkoutTimed_WLWorkout]
	foreign key ([workout_id], [workoutType_id]) references [dbo].[WLWorkout] ([workout_id], [workoutType_id])
	on delete cascade
	on update cascade
GO

--------------------------------------------------------------------------------
-- WLWorkoutTimed_Movement                                                    --
--------------------------------------------------------------------------------
create table [dbo].[WLWorkoutTimed_Movement](
	[workout_id] int not null,
	[workoutType_id] int not null,
	[movement_id] int not null,
	[roundNumber] int not null,
	constraint [PK_WLWorkoutTimed_Movement] primary key clustered ([workout_id] asc, [workoutType_id], [movement_id]),
	constraint [UCK_WLWorkoutTimed_Movement_CK1] unique ([movement_id]),
	constraint [UCK_WLWorkoutTimed_Movement_CK2] unique ([workout_id], [roundNumber])
)
GO
alter table [dbo].[WLWorkoutTimed_Movement] add constraint [CHK_WLWorkoutTimed_Movement_Cond1]
	check ([workoutType_id] = 2)
GO
alter table [dbo].[WLWorkoutTimed_Movement] add constraint [FK_WLWorkoutTimed_Movement_WLWorkoutTimed]
	foreign key ([workout_id], [workoutType_id]) references [dbo].[WLWorkoutTimed] ([workout_id], [workoutType_id])
	on delete cascade
	on update cascade
GO
alter table [dbo].[WLWorkoutTimed_Movement] add constraint [FK_WLWorkoutTimed_Movement_WLMovement]
	foreign key ([movement_id]) references [dbo].[WLMovement] ([movement_id])
	on delete cascade
	on update cascade
GO

--------------------------------------------------------------------------------
-- WLResultType                                                               --
--------------------------------------------------------------------------------
create table [dbo].[WLResultType](
	[resultType_id] int not null,
	[tableName] nvarchar(128) not null,
	constraint [PK_WLResultType] primary key clustered (resultType_id asc),
	constraint [UCK_WLResultType_C1] unique (tableName)
)
go

--------------------------------------------------------------------------------
-- WLResult                                                                   --
--------------------------------------------------------------------------------
create table [dbo].[WLResult](
	[result_id] int not null,
	[resultType_id] int not null,
	constraint [PK_WLResult] primary key clustered ([result_id] asc),
	constraint [UCK_WlResult_C1] unique ([result_id], [resultType_id])
)
go
alter table [dbo].[WLResult] add constraint [FK_WLResult_ResultType] 
	foreign key ([resultType_id]) references [dbo].[WLResultType]([resultType_id])
	on delete no action
	on update no action
go

--------------------------------------------------------------------------------
-- WLResultDescriptive                                                        --
--------------------------------------------------------------------------------

create table [dbo].[WLResultDescriptive](
	[result_id] int not null,
	[resultType_id] int not null,
	[description] text not null default '',
	constraint [PK_WLResultDescriptive] primary key clustered ([result_id] asc ,[resultType_id]),
)
GO
insert into [dbo].[WLResultType] ([resultType_id], [tableName]) values (1, 'WLResultDescriptive')
GO
alter table [dbo].[WLResultDescriptive] add constraint [CHK_WLResultDescriptive_Cond1] 
	check ([resultType_id] = 1)
GO
alter table [dbo].[WLResultDescriptive] add constraint [FK_WLResultDescriptive_WLResult]
	foreign key ([result_id], [resultType_id]) references [dbo].[WLResult]([result_id], [resultType_id])
	on delete cascade
	on update cascade
GO

--------------------------------------------------------------------------------
-- WLResultTimed                                                              --
--------------------------------------------------------------------------------

create table [dbo].[WLResultTimed](
	[result_id] int not null,
	[resultType_id] int not null,
	[completedIn] time not null default '00:00:00',
	constraint [PK_WLResultTimed] primary key clustered ([result_id] asc ,[resultType_id]),
)
GO
insert into [dbo].[WLResultType] ([resultType_id], [tableName]) values (2, 'WLResultTimed')
GO
alter table [dbo].[WLResultTimed] add constraint [CHK_WLResultTimed_Cond1] 
	check ([resultType_id] = 2)
GO
alter table [dbo].[WLResultTimed] add constraint [FK_WLResultTimed_WLResult]
	foreign key ([result_id], [resultType_id]) references [dbo].[WLResult]([result_id], [resultType_id])
	on delete cascade
	on update cascade
GO

--------------------------------------------------------------------------------
-- WLWorkoutType_ResultType                                                   --
--------------------------------------------------------------------------------

create table [dbo].[WLWorkoutType_ResultType](
	[workoutType_id] int not null,
	[resultType_id] int not null,
	constraint [PK_WLWorkoutType_ResultType] primary key clustered ([workoutType_id] asc, [resultType_id])
)
GO
alter table [dbo].[WLWorkoutType_ResultType] add constraint [FK_WLWorkoutType_ResultType_WorkoutType]
	foreign key ([workoutType_id]) references [dbo].[WLWorkoutType]([workoutType_id])
	on delete cascade
	on update cascade
GO
alter table [dbo].[WLWorkoutType_ResultType] add constraint [FK_WLWorkoutType_ResultType_ResultType]
	foreign key ([resultType_id]) references [dbo].[WLResultType]([resultType_id])
	on delete cascade
	on update cascade
GO
insert into [dbo].[WLWorkoutType_ResultType] (workoutType_id, resultType_id) values (1,1)
GO
insert into [dbo].[WLWorkoutType_ResultType] (workoutType_id, resultType_id) values (2,1)
GO
insert into [dbo].[WLWorkoutType_ResultType] (workoutType_id, resultType_id) values (2,2)
GO

--------------------------------------------------------------------------------
-- WLAccount                                                                  --
--------------------------------------------------------------------------------

create table [dbo].[WLAccount](
	[account_id] int not null,
	constraint [PK_WLAccount] primary key clustered ([account_id] asc)
)
GO

--------------------------------------------------------------------------------
-- WLLog                                                                      --
--------------------------------------------------------------------------------

create table [dbo].[WLLog](
	[log_id] int not null,
	[workout_id] int not null,
	[workoutType_id] int not null,
	[result_id] int not null,
	[resultType_id] int not null,
	[account_id] int not null,
	[dateCompleted] date not null default '1/1/1900',
	constraint [PK_WLLog] primary key clustered ([log_id] asc),
	constraint [UCK_WLLog_C1] unique ([result_id])
)
GO
alter table [dbo].[WLLog] add constraint [FK_WLLog_Workout]
	foreign key ([workout_id], [workoutType_id]) references [dbo].[WLWorkout]([workout_id], [workoutType_id])
	on delete no action
	on update no action
GO
alter table [dbo].[WLLog] add constraint [FK_WLLog_Result]
	foreign key ([result_id], [resultType_id]) references [dbo].[WLResult]([result_id], [resultType_id])
	on delete no action
	on update no action
GO
alter table [dbo].[WLLog] add constraint [FK_WLLog_WorkoutType_ResultType]
	foreign key ([workoutType_id], [resultType_id]) references [dbo].[WLWorkoutType_ResultType]([workoutType_id], [resultType_id])
	on delete no action
	on update no action
GO

